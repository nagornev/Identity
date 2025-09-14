using FluentValidation;

namespace Auth.Api.Contracts
{
    public class RequestPasswordChangeContractValidator : AbstractValidator<RequestPasswordChangeContract>
    {
        public RequestPasswordChangeContractValidator()
        {
            RuleFor(x => x.OldPassword).NotNull()
                                       .WithMessage("The old password can`t be null.");
            RuleFor(x => x.OldPassword).NotEmpty()
                                       .WithMessage("The old password can`t be empty.");

            RuleFor(x => x.NewPassword).NotNull()
                                       .WithMessage("The new password can`t be null.");
            RuleFor(x => x.NewPassword).NotEmpty()
                                       .WithMessage("The new password can`t be empty.");
        }
    }
}
