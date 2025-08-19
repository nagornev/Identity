using Auth.Application.Abstractions.Events;
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

        private readonly IDomainEventDispatcher _domainEventDispatcher;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public OutboxService(IOutboxRepository outboxRepository,
                             IDomainEventDispatcher domainEventDispatcher,
                             ITimeProvider timeProvider,
                             IUnitOfWork unitOfWork)
        {
            _outboxRepository = outboxRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
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
                await _domainEventDispatcher.DispatchAsync(domainEvent);

                outboxMessage.MarkAsProccesed();
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                //TODO: log error
            }
        }
    }
}
