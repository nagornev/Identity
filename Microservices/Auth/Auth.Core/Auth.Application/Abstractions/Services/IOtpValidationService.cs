using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Services
{
    public interface IOtpValidationService
    {
        Task<OtpContent> ValidateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default);
    }
}
