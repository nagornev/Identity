namespace Notification.Application.Abstractions.Services
{
    public interface IDeleteExpiredPendingNotificationMessageBackgroundService
    {
        Task DeleteAsync(CancellationToken cancellation = default);
    }
}
