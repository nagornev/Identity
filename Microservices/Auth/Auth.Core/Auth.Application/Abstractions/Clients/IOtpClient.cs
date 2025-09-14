using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Clients
{
    public interface IOtpClient
    {
        /// <summary>
        /// Creates OTP record for registrated user in the server and returns OTP ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tag"></param>
        /// <param name="payload"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Otp> CreateAsync(Guid userId, string tag, string payload = "", CancellationToken cancellation = default);

        /// <summary>
        /// Validates OTP from user.
        /// </summary>
        /// <param name="otpToken"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<OtpValidation> ValidateAsync(Guid otpId, string otp, string tag, CancellationToken cancellation = default);
    }
}
