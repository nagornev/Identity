namespace Notification.Application.Abstractions.Services
{
    public interface IPendingNotificationMessageSendService
    {
        Task SendAsync(Guid pendingNotificationMessageId, CancellationToken cancellation = default);
    }
}
