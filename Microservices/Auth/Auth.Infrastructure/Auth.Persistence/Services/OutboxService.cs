using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Persistence.Abstractions.Repositories;
using Auth.Persistence.Entities;
using DDD.Events;
using DDD.Repositories;
using Newtonsoft.Json;

namespace Auth.Persistence.Services
{
    public class OutboxService : IOutboxService
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
        };

        private readonly IOutboxRepository _outboxRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ITimeProvider _timeProvider;

        public OutboxService(IOutboxRepository outboxRepository,
                             IUnitOfWork unitOfWork,
                             ITimeProvider timeProvider)
        {
            _outboxRepository = outboxRepository;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public async Task HandleMessageAsync(CancellationToken cancellation = default)
        {
            OutboxMessage? outboxMessage = await _outboxRepository.LockNextOutboxMessageAsync(_timeProvider.NowUnix(),
                                                                                              cancellation);

            if (outboxMessage == null)
                return;

            IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Payload,
                                                                                   _jsonSerializerSettings)!;

            try
            {
                //await publisher.Publish(domainEvent);
                outboxMessage.MarkAsProccesed();
                await _unitOfWork.SaveAsync();
            }
            catch
            {

            }
        }
    }
}
