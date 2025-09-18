namespace Notification.Application.Abstractions.Services
{
    public interface IDeleteExpiredNotificationMessageBackgroundService
    {
        Task DeleteAsync(CancellationToken cancellation = default);
    }
}
