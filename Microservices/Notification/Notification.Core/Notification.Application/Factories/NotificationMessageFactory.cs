using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Factories;
using Notification.Application.Abstractions.Providers;
using Notification.Application.Options;
using Notification.Domain.Aggregates;

namespace Notification.Application.Factories
{
    public class NotificationMessageFactory : INotificationMessageFactory
    {
        private readonly ITimeProvider _timeProvider;

        private readonly NotificationMessageOptions _notificationMessageOptions;

        public NotificationMessageFactory(ITimeProvider timeProvider,
                                          IOptions<NotificationMessageOptions> notificationMessageOptions)
        {
            _timeProvider = timeProvider;
            _notificationMessageOptions = notificationMessageOptions.Value;
        }

        public NotificationMessage Create(Guid userId, string channelType, string channelValue, string type, string text)
        {
            long createdAt = _timeProvider.NowUnix();

            return NotificationMessage.Create(userId,
                                              channelType,
                                              channelValue,
                                              type,
                                              text,
                                              createdAt,
                                              createdAt + _notificationMessageOptions.Lifetime);
        }
    }
}
