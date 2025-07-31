using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInConfirmService
    {
        Task<AuthDto> ConfirmAsync(string otpToken, string otp, string publicKey, string device, string ipAddress, CancellationToken cancellation = default);
    }
}
