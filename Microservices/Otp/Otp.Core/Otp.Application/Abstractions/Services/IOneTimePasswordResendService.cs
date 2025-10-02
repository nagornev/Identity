namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordResendService
    {
        Task ResendAsync(Guid oneTimePasswordId, CancellationToken cancellation = default);
    }
}
