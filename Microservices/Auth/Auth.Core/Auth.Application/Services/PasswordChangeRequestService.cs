using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Consts;
using Auth.Application.DTOs;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class PasswordChangeRequestService : IPasswordChangeRequestService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IPasswordValidator _passwordValidator;

        private readonly IPasswordHashProvider _passwordHashProvider;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IOtpClient _otpClient;
        private readonly IOtpTokenPayloadProvider _otpTokenPayloadProvider;

        public PasswordChangeRequestService(IUserQueryService userQueryService,
                                            IPasswordValidator passwordValidator,
                                            IPasswordHashProvider passwordHashProvider,
                                            IUnitOfWork unitOfWork,
                                            IOtpClient otpClient,
                                            IOtpTokenPayloadProvider otpTokenPayloadProvider)
        {
            _userQueryService = userQueryService;
            _passwordValidator = passwordValidator;
            _passwordHashProvider = passwordHashProvider;
            _unitOfWork = unitOfWork;
            _otpClient = otpClient;
            _otpTokenPayloadProvider = otpTokenPayloadProvider;
        }

        public async Task<Otp> RequestAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByIdAsync(userId, cancellation);

            if (!_passwordValidator.Verify(oldPassword, user.Authentication.PasswordHash.Value, user.Authentication.PasswordSalt))
                throw new UserInvalidPasswordApplicationException(userId);

            user.ChangePassword(_passwordHashProvider.Hash(newPassword, user.Authentication.PasswordSalt));

            Otp otp = await _otpClient.CreateAsync(user.Id,
                                                   OtpTags.ChangePassword,
                                                   payload: _otpTokenPayloadProvider.Serialize(new ChangePasswordHashOtpTokenPayload(user.Authentication.PendingPasswordHash!.Version)),
                                                   cancellation: cancellation);

            await _unitOfWork.SaveAsync(cancellation);

            return otp;
        }
    }
}
