namespace Otp.Application.Abstractions.Clients
{
    public interface INotificationClient
    {
        Task OneTimePasswordNotificationAsync(Guid userId, string oneTimePasswordValue, string type, string channel, CancellationToken cancellation = default);
    }
}
