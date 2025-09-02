using Auth.Application.Abstractions.Services;
using Auth.Messaging.Abstractions.Providers;
using DDD.Events;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly IMessageContractsProvider _messageContractProvider;

        private readonly IPublishEndpoint _publishService;

        public PublishEventService(IMessageContractsProvider messageContractProvider, 
                                   IPublishEndpoint publishService)
        {
            _publishService = publishService;
            _messageContractProvider = messageContractProvider;
        }

        public async Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent
        {
            dynamic messageContract = await _messageContractProvider.Create(domainEvent);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
