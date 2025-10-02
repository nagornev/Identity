namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordCreatedEventService
    {
        Task HandleAsync(Guid oneTimePasswordId, CancellationToken cancellation = default);
    }
}
