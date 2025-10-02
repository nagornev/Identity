using Notification.Domain.Aggregates;

namespace Notification.Application.Abstractions.Services
{
    public interface INotificationMessageQueryService
    {
        Task<NotificationMessage> GetNotificationMessageByIdAsync(Guid notificationMessageId, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<NotificationMessage>> GetExpiredNotificationMessagesAsync(long timestamp, CancellationToken cancellation = default);
    }
}
