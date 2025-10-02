using Auth.Domain.ValueObjects;
using FluentValidation;

namespace Auth.Api.Contracts
{
    public class RequestUserSignUpContractValidator : AbstractValidator<RequestUserSignUpContract>
    {
        public RequestUserSignUpContractValidator()
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

            RuleFor(x => x.PersonName).NotNull()
                                         .WithMessage("The new person name can`t be null.");
            RuleFor(x => x.PersonName).NotEmpty()
                                         .WithMessage("The new person name can`t be empty.");

            When(x => x.PersonName != null, () =>
            {
                RuleFor(x => x.PersonName).Must(name => !string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
                                             .WithMessage("The file name can`t be null or empty.")
                                             .Must(name => name.Length < PersonName.MaximumLength && name.Length >= PersonName.MinimumLength)
                                             .WithMessage($"The file lenght can`t be less than {PersonName.MinimumLength} and more than {PersonName.MaximumLength} bytes.");
            });
        }
    }
}
