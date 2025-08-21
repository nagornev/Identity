namespace Auth.Application.Abstractions.Clients
{
    public interface INotificationClient
    {
        Task ChannelNotificationAsync(Guid userId, string channel, string token, CancellationToken cancellation = default);
    }
}
