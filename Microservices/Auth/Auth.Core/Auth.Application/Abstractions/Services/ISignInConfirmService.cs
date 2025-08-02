using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInConfirmService
    {
        Task<TokenPair> ConfirmAsync(string otpToken,
                                     string otp,
                                     string audience,
                                     string publicKey,
                                     string device,
                                     string ipAddress,
                                     CancellationToken cancellation = default);
    }
}
