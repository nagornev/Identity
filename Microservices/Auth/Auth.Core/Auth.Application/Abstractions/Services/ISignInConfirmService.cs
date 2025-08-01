namespace Auth.Application.Abstractions.Services
{
    public interface ISignInConfirmService
    {
        Task<DTOs.AuthTokens> ConfirmAsync(string otpToken, string otp, string publicKey, string device, string ipAddress, CancellationToken cancellation = default);
    }
}
