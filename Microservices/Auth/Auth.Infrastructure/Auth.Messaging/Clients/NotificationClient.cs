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

        public async Task ActivateNotificationAsync(Guid userId, string channelValue, string url, CancellationToken cancellation = default)
        {
            //_logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n" +
            //                       $"Activate notification for {channelValue}\n" +
            //                       $"Token: {url}\n" +
            //                       $"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");

            await _publishService.Publish(new ActivateNotificationMessageContract(userId, url, ChannelTypes.Email, channelValue), cancellation);
        }

        public async Task EmailChannelNotificationAsync(Guid userId, string channelValue, string url, CancellationToken cancellation = default)
        {
            //_logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n" +
            //                       $"Channel notification for {channelValue}\n" +
            //                       $"Token: {url}\n" +
            //                       $"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");

            await _publishService.Publish(new ChannelNotificationMessageContract(userId, url, ChannelTypes.Email, channelValue), cancellation);
        }
    }
}
