using Notification.Domain.Aggregates;

namespace Notification.Application.Abstractions.Services
{
    public interface IPendingNotificationMessageQueryService
    {
        Task<IReadOnlyCollection<PendingNotificationMessage>> GetPendingNotificationMessagesByUserIdAsync(Guid userId, CancellationToken cancellation = default);

        Task<PendingNotificationMessage> GetPendingNotificationMessageByIdAsync(Guid pendingNotificationMessageId, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<PendingNotificationMessage>> GetExpiredPendingNotificaitonMessagesAsync(long timestamp, CancellationToken cancellation = default);
    }
}
