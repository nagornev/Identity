using Auth.Application.Abstractions.Services;
using Auth.Messaging.Abstractions.Providers;
using DDD.Events;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly IPublishEndpoint _publishService;

        private readonly IMessageContractProvider _messageContractProvider;

        public PublishEventService(IPublishEndpoint publishService,
                                   IMessageContractProvider messageContractProvider)
        {
            _publishService = publishService;
            _messageContractProvider = messageContractProvider;
        }

        public async Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent
        {
            IMessageContract messageContract = _messageContractProvider.Create(domainEvent);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
