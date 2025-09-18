using Microsoft.Extensions.Options;
using Notification.Application.Abstractions.Factories;
using Notification.Application.Abstractions.Providers;
using Notification.Application.Options;
using Notification.Domain.Aggregates;

namespace Notification.Application.Factories
{
    public class PendingNotificationMessageFactory : IPendingNotificationMessageFactory
    {
        private readonly ITimeProvider _timeProvider;

        private readonly PendingNotificationMessageOptions _pendingNotificationMessageOptions;

        public PendingNotificationMessageFactory(ITimeProvider timeProvider,
                                                 IOptions<PendingNotificationMessageOptions> pendingNotificationMessageOptions)
        {
            _timeProvider = timeProvider;
            _pendingNotificationMessageOptions = pendingNotificationMessageOptions.Value;
        }

        public PendingNotificationMessage Create(Guid userId, string channelType, string channelValue, string type, string text)
        {
            long createdAt = _timeProvider.NowUnix();

            return PendingNotificationMessage.Create(userId,
                                                     channelType,
                                                     channelValue,
                                                     type,
                                                     text,
                                                     createdAt,
                                                     createdAt + _pendingNotificationMessageOptions.Lifetime);
        }
    }
}
