using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class EmailAddressChangeRequestService : IEmailAddressChangeRequestService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOtpClient _otpClient;

        private readonly IOtpTokenPayloadProvider _otpTokenPayloadProvider;

        public EmailAddressChangeRequestService(IUserQueryService userQueryService,
                                                IUnitOfWork unitOfWork,
                                                IOtpClient otpClient,
                                                IOtpTokenPayloadProvider otpTokenPayloadProvider)
        {
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
            _otpClient = otpClient;
            _otpTokenPayloadProvider = otpTokenPayloadProvider;
        }

        public async Task<Otp> RequestAsync(Guid userId, string emailAddress, CancellationToken cancellation)
        {
            if (await _userQueryService.IsUserAlreadyExistsAsync(emailAddress, cancellation))
                throw new UserInvalidEmailAddressApplicationException(emailAddress);

            User user = await _userQueryService.GetUserByIdAsync(userId, cancellation);
            user.ChangeEmailAddress(emailAddress);

            Otp otp = await _otpClient.CreateAsync(user.Id,
                                                   OtpTags.ChangeEmailAddress,
                                                   payload: _otpTokenPayloadProvider.Serialize(new ChangeEmailAddressOtpTokenPayload(user.Profile.PendingEmailAddress!.Version)),
                                                   cancellation: cancellation);

            await _unitOfWork.SaveAsync(cancellation);

            return otp;
        }
    }
}
