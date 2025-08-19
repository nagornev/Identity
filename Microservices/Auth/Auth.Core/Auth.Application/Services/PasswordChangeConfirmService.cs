using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class PasswordChangeConfirmService : IPasswordChangeConfirmService
    {
        private readonly IOtpValidationService _otpValidationService;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public PasswordChangeConfirmService(IOtpValidationService otpValidationService,
                                            IUserQueryService userQueryService,
                                            IUnitOfWork unitOfWork)
        {
            _otpValidationService = otpValidationService;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(string otpToken, string otp, CancellationToken cancellation = default)
        {
            OtpContent otpContent = await _otpValidationService.ValidateAsync(otpToken, otp, OtpTags.ChangePassword, cancellation);

            User user = await _userQueryService.GetUserByIdAsync(otpContent.Subject, cancellation);
            user.ConfirmPassword();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
