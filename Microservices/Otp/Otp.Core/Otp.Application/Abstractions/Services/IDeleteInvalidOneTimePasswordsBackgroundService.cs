namespace Otp.Application.Abstractions.Services
{
    public interface IDeleteInvalidOneTimePasswordsBackgroundService
    {
        Task DeleteAsync(CancellationToken cancellation = default);
    }
}
