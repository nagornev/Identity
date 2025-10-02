using DDD.Events;
using DDD.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Abstractions.Services;
using Otp.Persistence.Abstractions.Repositories;
using Otp.Persistence.Entities;

namespace Otp.Persistence.Services
{
    public class OutboxService : IOutboxService
    {
        private const int _emptyOutboxBatchDelay = 5000;

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
        };

        private readonly IOutboxRepository _outboxRepository;

        private readonly IPublishEventService _publishEventService;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<OutboxService> _logger;

        public OutboxService(IOutboxRepository outboxRepository,
                             IPublishEventService publishEventService,
                             ITimeProvider timeProvider,
                             IUnitOfWork unitOfWork,
                             ILogger<OutboxService> logger)
        {
            _outboxRepository = outboxRepository;
            _publishEventService = publishEventService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task HandleAsync(CancellationToken cancellation = default)
        {
            IReadOnlyCollection<OutboxMessage> outboxBatch = await _outboxRepository.LockNextOutboxBatchAsync(_timeProvider.NowUnix(),
                                                                                                              cancellation);

            if (outboxBatch.Count < 1)
            {
                await Task.Delay(_emptyOutboxBatchDelay, cancellation);
                return;
            }

            await Task.WhenAll(outboxBatch.Select(outboxMessage => Task.Run(async () =>
            {
                try
                {
                    IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Payload,
                                                                                           _jsonSerializerSettings)!;

                    await _publishEventService.PublishAsync(domainEvent);
                    outboxMessage.MarkAsProccesed();
                }
#if DEBUG
                catch (NotSupportedException)
                {
                    outboxMessage.MarkAsProccesed();
                }
#endif
                catch (Exception exception)
                {
                    _logger?.LogError(exception, exception.Message);
                }
            })));

            await _unitOfWork.SaveAsync();
        }
    }
}
