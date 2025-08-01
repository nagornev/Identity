using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Consts;
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

        public PasswordChangeRequestService(IUserQueryService userQueryService,
                                            IPasswordValidator passwordValidator,
                                            IPasswordHashProvider passwordHashProvider,
                                            IUnitOfWork unitOfWork,
                                            IOtpClient otpClient)
        {
            _userQueryService = userQueryService;
            _passwordValidator = passwordValidator;
            _passwordHashProvider = passwordHashProvider;
            _unitOfWork = unitOfWork;
            _otpClient = otpClient;
        }

        public async Task<string> RequestAsync(Guid userId, string oldPassword, string newPassword, CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByIdAsync(userId, cancellation);

            if (_passwordValidator.Verify(oldPassword, user.Authentication.PasswordHash.Value))
                throw new UserInvalidPasswordApplicationException(userId);

            user.ChangePassword(_passwordHashProvider.Hash(newPassword));

            await _unitOfWork.SaveAsync(cancellation);

            return await _otpClient.CreateAsync(user.Id, OtpTags.ChangePassword, cancellation);
        }
    }
}
