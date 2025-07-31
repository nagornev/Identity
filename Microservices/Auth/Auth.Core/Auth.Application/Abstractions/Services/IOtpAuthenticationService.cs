using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IOtpAuthenticationService
    {
        Task<User> AuthenticateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default);
    }
}
