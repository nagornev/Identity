namespace Auth.Application.Abstractions.Clients
{
    public interface INotificationClient
    {
        Task ActivateNotificationAsync(Guid userId, string channelValue, string url, CancellationToken cancellation = default);

        Task EmailChannelNotificationAsync(Guid userId, string channelValue, string url, CancellationToken cancellation = default);
    }
}
