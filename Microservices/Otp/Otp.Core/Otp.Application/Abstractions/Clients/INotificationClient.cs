namespace Otp.Application.Abstractions.Clients
{
    public interface INotificationClient
    {
        Task OneTimePasswordNotificationAsync(Guid subject, string oneTimePasswordValue, CancellationToken cancellation = default);
    }
}
