namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordUsedEventService
    {
        Task HandleAsync(Guid oneTimePasswordId, CancellationToken cancellation = default);
    }
}
