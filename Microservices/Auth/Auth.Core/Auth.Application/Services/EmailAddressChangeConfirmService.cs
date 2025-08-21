using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Security;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class EmailAddressChangeConfirmService : IEmailAddressChangeConfirmService
    {
        private readonly IOtpValidationService _otpValidationService;

        private readonly IOtpTokenPayloadProvider _otpTokenPayloadProvider;

        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public EmailAddressChangeConfirmService(IOtpValidationService otpValidationService,
                                                IOtpTokenPayloadProvider otpTokenPayloadProvider,
                                                IUserQueryService userQueryService,
                                                IUnitOfWork unitOfWork)
        {
            _otpValidationService = otpValidationService;
            _otpTokenPayloadProvider = otpTokenPayloadProvider;
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ConfirmAsync(string otpToken, string otp, CancellationToken cancellation = default)
        {
            OtpContent otpContent = await _otpValidationService.ValidateAsync(otpToken, otp, OtpTags.ChangeEmailAddress, cancellation);
            ChangeEmailAddressOtpTokenPayload changeEmailAddressOtpTokenPayload = _otpTokenPayloadProvider.Deserialize<ChangeEmailAddressOtpTokenPayload>(otpContent.Payload);

            User user = await _userQueryService.GetUserByIdAsync(otpContent.Subject, cancellation);

            if (user.Profile.PendingEmailAddress?.Version != changeEmailAddressOtpTokenPayload.Version)
                throw new PendingEmailAddressVersionInvalidApplicationException(user.Id);

            user.ConfirmEmailAddressChange();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
