using DDD.Events;
using MassTransit;
using Notification.Application.Abstractions.Services;
using Notification.Messaging.Abstractions.Providers;

namespace Notification.Messaging.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly IPublishEndpoint _publishService;

        private readonly IMessageContractsProvider _messageContractsProvider;

        public PublishEventService(IPublishEndpoint publishService,
                                   IMessageContractsProvider messageContractsProvider)
        {
            _publishService = publishService;
            _messageContractsProvider = messageContractsProvider;
        }

        public async Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent
        {
            dynamic messageContract = await _messageContractsProvider.CreateAsync(domainEvent);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
