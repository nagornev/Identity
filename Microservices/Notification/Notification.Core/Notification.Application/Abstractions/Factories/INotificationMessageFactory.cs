using Notification.Domain.Aggregates;

namespace Notification.Application.Abstractions.Factories
{
    public interface INotificationMessageFactory
    {
        NotificationMessage Create(Guid userId, string channelType, string channelValue, string type, string text);
    }
}
