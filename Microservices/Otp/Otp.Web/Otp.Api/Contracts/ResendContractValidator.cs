using FluentValidation;

namespace Otp.Api.Contracts
{
    public class ResendContractValidator : AbstractValidator<ResendContract>
    {
        public ResendContractValidator()
        {
            RuleFor(x => x.OneTimePasswordId).NotNull()
                                             .WithMessage("The one time password ID can`t be null.");
            RuleFor(x => x.OneTimePasswordId).NotEmpty()
                                             .WithMessage("The one time password ID can`t be empty.");
        }
    }
}
