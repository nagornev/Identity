using Auth.Application.Abstractions.Providers;
using Auth.Application.Converters;
using FluentValidation;

namespace Auth.Api.Contracts
{
    public class RefreshContractValidator : AbstractValidator<RefreshContract>
    {
        private const int _publicKeyLength = 32;

        private const int _window = 300;//5 minutes

        public RefreshContractValidator(ITimeProvider timeProvider)
        {
            RuleFor(x => x.RefreshToken).NotNull()
                                        .WithMessage("The refresh token can`t be null.");
            RuleFor(x => x.RefreshToken).NotEmpty()
                                        .WithMessage("The refresh token can`t be empty.");
            RuleFor(x => x.RefreshToken).Matches(@"^(?:[A-Za-z0-9_-]{2,}=*)\.(?:[A-Za-z0-9_-]{2,}=*)\.(?:[A-Za-z0-9_-]{1,}=*)$")
                                        .WithMessage("The refresh token has invalid format.");

            RuleFor(x => x.NewPublicKey).NotNull()
                                        .WithMessage("The new public key can`t be null.");
            RuleFor(x => x.NewPublicKey).NotEmpty()
                                        .WithMessage("The new public key can`t be empty.");
            When(x => x.NewPublicKey != null, () =>
            {
                RuleFor(x => x.NewPublicKey).Must(publicKey => Base64UrlConverter.FromBase64Url(publicKey).Length == _publicKeyLength)
                                            .WithMessage($"The new public key does not consist of {_publicKeyLength} symbols.");
            });

            RuleFor(x => x.Timestamp).NotNull()
                                     .WithMessage("The timestamp can`t be null.");
            RuleFor(x => x.Timestamp).NotEmpty()
                                     .WithMessage("The timestamp can`t be empty.");
            RuleFor(x => x.Timestamp).Must(timestamp => timeProvider.NowUnix() - timestamp < _window)
                                     .WithMessage($"The timestamp is invalid.");

            RuleFor(x => x.Signature).NotNull()
                                     .WithMessage("The signature can`t be null.");
            RuleFor(x => x.Signature).NotEmpty()
                                     .WithMessage("The signature can`t be empty.");
        }
    }
}
