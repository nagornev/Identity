using MassTransit;
using Otp.Application.Abstractions.Services;
using Otp.Messaging.Abstractions.Providers;

namespace Otp.Messaging.Services
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

        async Task IPublishEventService.PublishAsync<T>(T domainEvent, CancellationToken cancellation)
        {
            dynamic messageContract = await _messageContractsProvider.CreateAsync(domainEvent, cancellation);

            await _publishService.Publish(messageContract, cancellation);
        }
    }
}
