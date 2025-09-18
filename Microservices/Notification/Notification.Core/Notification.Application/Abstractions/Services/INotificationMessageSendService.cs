namespace Notification.Application.Abstractions.Services
{
    public interface INotificationMessageSendService
    {
        Task SendAsync(Guid notificationMessageId, CancellationToken cancellation = default);
    }
}
