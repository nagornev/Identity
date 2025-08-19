using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Clients
{
    public interface IOtpClient
    {
        /// <summary>
        /// Creates OTP record for registrated user in the server and returns OTP token.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="tag"></param>
        /// <param name="payload"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<string> CreateAsync(Guid subject, string tag, string payload = "", CancellationToken cancellation = default);

        /// <summary>
        /// Validates OTP from user.
        /// </summary>
        /// <param name="otpToken"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<OtpValidation> ValidateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default);
    }
}
