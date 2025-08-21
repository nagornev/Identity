using Auth.Application.Abstractions.Clients;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Clients
{
    public class NotificationClient : INotificationClient
    {
        private readonly IPublishEndpoint _publishService;

        public NotificationClient(IPublishEndpoint publishService)
        {
            _publishService = publishService;
        }

        public async Task ChannelNotificationAsync(Guid userId, string channel, string token, CancellationToken cancellation = default)
        {
            await _publishService.Publish(new ChannelNotificationMessageContract(userId, channel, token));
        }
    }
}
