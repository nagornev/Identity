namespace Auth.Application.Abstractions.Clients
{
    public interface IOtpClient
    {
        /// <summary>
        /// Creates OTP record in the server and returns OTP token.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<string> CreateAsync(Guid userId, string tag, CancellationToken cancellation = default);

        /// <summary>
        /// Validates OTP from user and returns user ID if validate result is true.
        /// </summary>
        /// <param name="otpToken"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<Guid> ValidateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default);
    }
}
