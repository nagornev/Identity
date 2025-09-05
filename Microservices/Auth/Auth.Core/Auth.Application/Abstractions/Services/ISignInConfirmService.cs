using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInConfirmService
    {
        Task<TokenPair> ConfirmAsync(Guid otpId,
                                     string otp,
                                     string newPublicKey,
                                     long timestamp,
                                     string signature,
                                     CancellationToken cancellation = default);
    }
}
