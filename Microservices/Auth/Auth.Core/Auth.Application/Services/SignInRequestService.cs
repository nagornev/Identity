using Auth.Application.Abstractions.Clients;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Consts;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class SignInRequestService : ISignInRequestService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IUserValidator _userValidator;

        private readonly IPasswordValidator _passwordValidator;

        private readonly IOtpClient _otpClient;

        public SignInRequestService(IUserQueryService userQueryService,
                                 IUserValidator userValidator,
                                 IPasswordValidator passwordValidator,
                                 IOtpClient otpClient)
        {
            _userQueryService = userQueryService;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _otpClient = otpClient;
        }

        public async Task<string> RequestAsync(string emailAddress, string password, CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByEmailAsync(emailAddress, cancellation);

            if (!_userValidator.Validate(user))
                throw new UserInvalidApplicationException(emailAddress);

            if (!_passwordValidator.Verify(password, user.Authentication.PasswordHash.Value))
                throw new UserInvalidPasswordApplicationException(emailAddress);

            return await _otpClient.CreateAsync(user.Id, OtpTags.SignIn);
        }
    }
}
