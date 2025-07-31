using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class OtpAuthenticationService : IOtpAuthenticationService
    {
        private readonly IOtpClient _otpClient;

        private readonly IUserQueryService _userQueryService;

        public OtpAuthenticationService(IOtpClient otpClient,
                                        IUserQueryService userQueryService)
        {
            _otpClient = otpClient;
            _userQueryService = userQueryService;
        }

        public async Task<User> AuthenticateAsync(string otpToken, string otp, string tag, CancellationToken cancellation = default)
        {
            Guid userId = await _otpClient.ValidateAsync(otpToken, otp, tag, cancellation);

            return await _userQueryService.GetUserByIdAsync(userId, cancellation);
        }
    }
}
