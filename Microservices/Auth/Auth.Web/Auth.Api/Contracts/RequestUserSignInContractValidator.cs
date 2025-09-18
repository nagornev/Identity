using Auth.Application.Converters;
using FluentValidation;

namespace Auth.Api.Contracts
{
    public class RequestUserSignInContractValidator : AbstractValidator<RequestUserSignInContract>
    {
        private const int _publicKeyLength = 32;

        public RequestUserSignInContractValidator()
        {
            RuleFor(x => x.EmailAddress).NotNull()
                                        .WithMessage("The email address can`t be null.");
            RuleFor(x => x.EmailAddress).NotEmpty()
                                        .WithMessage("The email address can`t be empty.");
            RuleFor(x => x.EmailAddress).EmailAddress()
                                        .WithMessage("The email address has invalid format.");

            RuleFor(x => x.Password).NotNull()
                                    .WithMessage("The password can`t be null.");
            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("The password can`t be empty.");

            RuleFor(x => x.Audience).NotNull()
                                    .WithMessage("The audience can`t be null.");
            RuleFor(x => x.Audience).NotEmpty()
                                    .WithMessage("The audience can`t be empty.");

            RuleFor(x => x.PublicKey).NotNull()
                                        .WithMessage("The public key can`t be null.");
            RuleFor(x => x.PublicKey).NotEmpty()
                                        .WithMessage("The public key can`t be empty.");
            When(x => x.PublicKey != null, () =>
            {
                RuleFor(x => x.PublicKey).Must(publicKey => Base64UrlConverter.FromBase64Url(publicKey).Length == _publicKeyLength)
                                            .WithMessage($"The public key does not consist of {_publicKeyLength} symbols.");
            });
        }
    }
}
