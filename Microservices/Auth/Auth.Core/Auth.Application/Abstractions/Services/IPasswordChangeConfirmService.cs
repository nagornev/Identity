namespace Auth.Application.Abstractions.Services
{
    public interface IPasswordChangeConfirmService
    {
        Task ConfirmAsync(string otpToken, string otp, CancellationToken cancellation = default);
    }
}
