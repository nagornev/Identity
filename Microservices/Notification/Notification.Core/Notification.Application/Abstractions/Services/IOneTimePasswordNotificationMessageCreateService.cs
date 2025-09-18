namespace Notification.Application.Abstractions.Services
{
    public interface IOneTimePasswordNotificationMessageCreateService
    {
        Task CreateAsync(Guid userId, string channelType, string channelValue, string oneTimePasswordValue, CancellationToken cancellation = default);
    }
}
