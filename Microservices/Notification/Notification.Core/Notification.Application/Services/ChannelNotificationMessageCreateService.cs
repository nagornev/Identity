using Notification.Application.Abstractions.Services;
using Notification.Domain.Consts;

namespace Notification.Application.Services
{
    public class ChannelNotificationMessageCreateService : IChannelNotificationMessageCreateService
    {
        private readonly INotificationMessageCreateService _notificationMessageCreateService;

        public ChannelNotificationMessageCreateService(INotificationMessageCreateService notificationMessageCreateService)
        {
            _notificationMessageCreateService = notificationMessageCreateService;
        }

        public async Task CreateAsync(Guid userId, string channelType, string channelValue, string url, CancellationToken cancellation = default)
        {
            await _notificationMessageCreateService.CreateAsync(userId,
                                                                channelType,
                                                                channelValue,
                                                                NotificationType.Channel,
                                                                url,
                                                                cancellation);
        }
    }
}
