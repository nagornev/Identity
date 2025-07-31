using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class PasswordChangeConfirmService : IPasswordChangeConfirmService
    {
        private readonly IOtpAuthenticationService _otpAuthenticationService;

        private readonly IUnitOfWork _unitOfWork;

        public PasswordChangeConfirmService(IOtpAuthenticationService otpAuthenticationService,
                                            IUnitOfWork unitOfWork)
        {
            _otpAuthenticationService = otpAuthenticationService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(string otpToken, string otp, CancellationToken cancellation = default)
        {
            User user = await _otpAuthenticationService.AuthenticateAsync(otpToken, otp, OtpTags.ChangePassword, cancellation);
            user.ConfirmPassword();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
