namespace Notification.Application.Abstractions.Services
{
    public interface IChannelNotificationMessageCreateService
    {
        Task CreateAsync(Guid userId, string channelType, string channelValue, string url, CancellationToken cancellation = default);
    }
}
