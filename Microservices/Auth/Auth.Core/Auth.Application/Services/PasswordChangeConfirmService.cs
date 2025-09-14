using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class PasswordChangeConfirmService : IPasswordChangeConfirmService
    {
        private readonly IOtpValidationService _otpValidationService;

        private readonly IOtpTokenPayloadProvider _otpTokenPayloadProvider;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public PasswordChangeConfirmService(IOtpValidationService otpValidationService,
                                            IOtpTokenPayloadProvider otpTokenPayloadProvider,
                                            IUserQueryService userQueryService,
                                            IUnitOfWork unitOfWork)
        {
            _otpValidationService = otpValidationService;
            _otpTokenPayloadProvider = otpTokenPayloadProvider;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(Guid otpId, string otp, CancellationToken cancellation = default)
        {
            OtpContent otpContent = await _otpValidationService.ValidateAsync(otpId, otp, OtpTags.ChangePassword, cancellation);
            ChangePasswordHashOtpTokenPayload changePasswordHashOtpTokenPayload = _otpTokenPayloadProvider.Deserialize<ChangePasswordHashOtpTokenPayload>(otpContent.Payload);

            User user = await _userQueryService.GetUserByIdAsync(otpContent.UserId, cancellation);

            if (user.Authentication.PendingPasswordHash?.Version != changePasswordHashOtpTokenPayload.Version)
                throw new PendingPasswordVersionInvalidApplicationException(user.Id);

            user.ConfirmPassword();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
