using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInConfirmService
    {
        Task<TokenPair> ConfirmAsync(string otpToken,
                                     string otp,
                                     string newPublicKey,
                                     long timestamp,
                                     string signature,
                                     CancellationToken cancellation = default);
    }
}
