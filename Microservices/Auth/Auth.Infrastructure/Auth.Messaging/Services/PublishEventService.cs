using Auth.Application.Abstractions.Services;
using Auth.Messaging.Abstractions.Providers;
using DDD.Events;
using MassTransit;

namespace Auth.Messaging.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly IMessageContractsProvider _messageContractsProvider;

        private readonly IPublishEndpoint _publishService;

        public PublishEventService(IMessageContractsProvider messageContractProvider,
                                   IPublishEndpoint publishService)
        {
            _publishService = publishService;
            _messageContractsProvider = messageContractProvider;
        }

        public async Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent
        {
            dynamic messageContract = await _messageContractsProvider.CreateAsync(domainEvent);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
