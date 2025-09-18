namespace Notification.Application.Abstractions.Services
{
    public interface IActivateNotificationMessageCreateService
    {
        Task CreateAsync(Guid userId, string channelType, string channelValue, string url, CancellationToken cancellation = default);
    }
}
