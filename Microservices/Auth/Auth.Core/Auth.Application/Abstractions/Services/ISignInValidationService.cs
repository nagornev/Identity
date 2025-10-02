using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInValidationService
    {
        void ValidateWindow(long timestamp, int window);

        void ValidateSession(Session session);

        void ValidateFingerprint(Guid otpId, string otp, string newPublicKey, long timestamp, string signature, Session session);

        Task<OtpContent> ValidateOtpAsync(Guid otpId, string otp, CancellationToken cancellation = default);
    }
}
