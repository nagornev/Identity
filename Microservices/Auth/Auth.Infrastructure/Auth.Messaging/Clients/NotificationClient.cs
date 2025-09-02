using Auth.Application.Abstractions.Clients;
using MassTransit;
using MessageContracts;
using Microsoft.Extensions.Logging;

namespace Auth.Messaging.Clients
{
    public class NotificationClient : INotificationClient
    {
        private readonly IPublishEndpoint _publishService;
        private readonly ILogger<NotificationClient> _logger;

        public NotificationClient(IPublishEndpoint publishService, ILogger<NotificationClient> logger)
        {
            _publishService = publishService;
            _logger = logger;
        }

        public async Task ChannelNotificationAsync(Guid userId, string channel, string token, CancellationToken cancellation = default)
        {
            _logger.LogInformation(token);
            //await _publishService.Publish(new ChannelNotificationMessageContract(userId, channel, token));
        }
    }
}
