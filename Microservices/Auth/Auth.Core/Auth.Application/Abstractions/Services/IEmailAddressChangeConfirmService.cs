namespace Auth.Application.Abstractions.Services
{
    public interface IEmailAddressChangeConfirmService
    {
        Task ConfirmAsync(string otpToken, string otp, CancellationToken cancellation = default);
    }
}
