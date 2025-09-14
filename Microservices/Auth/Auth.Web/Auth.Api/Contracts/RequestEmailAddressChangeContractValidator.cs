using FluentValidation;

namespace Auth.Api.Contracts
{
    public class RequestEmailAddressChangeContractValidator : AbstractValidator<RequestEmailAddressChangeContract>
    {
        public RequestEmailAddressChangeContractValidator()
        {
            RuleFor(x => x.NewEmailAddress).NotNull()
                                           .WithMessage("The new email address can`t be null.");
            RuleFor(x => x.NewEmailAddress).NotEmpty()
                                           .WithMessage("The new email address can`t be empty.");
            RuleFor(x => x.NewEmailAddress).EmailAddress()
                                           .WithMessage("The new email address has invalid format.");
        }
    }
}
