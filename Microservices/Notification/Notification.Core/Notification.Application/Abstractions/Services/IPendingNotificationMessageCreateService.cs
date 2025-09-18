namespace Notification.Application.Abstractions.Services
{
    public interface IPendingNotificationMessageCreateService
    {
        Task CreateAsync(Guid userId, string channelType, string channelValue, string type, string text, CancellationToken cancellation = default);
    }
}
