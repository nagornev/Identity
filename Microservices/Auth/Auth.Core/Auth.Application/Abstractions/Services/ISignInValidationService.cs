using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface ISignInValidationService
    {
        void ValidateWindow(long timestamp, int window);

        void ValidateSession(Session session);

        void ValidateFingerprint(string otpToken, string otp, long timestamp, string signature, Session session);

        Task<OtpContent> ValidateOtpAsync(string otpToken, string otp, CancellationToken cancellation = default);
    }
}
