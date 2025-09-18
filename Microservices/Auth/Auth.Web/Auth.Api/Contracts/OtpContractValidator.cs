using FluentValidation;

namespace Auth.Api.Contracts
{
    public class OtpContractValidator : AbstractValidator<OtpContract>
    {
        private const int _otpLength = 6;

        public OtpContractValidator()
        {
            RuleFor(x => x.OtpId).NotNull()
                                 .WithMessage("The one time password ID can`t be null.");
            RuleFor(x => x.OtpId).NotEmpty()
                                 .WithMessage("The one time password ID can`t be empty.");

            RuleFor(x => x.Otp).NotNull()
                               .WithMessage("The one time password can`t be null.");
            RuleFor(x => x.Otp).NotEmpty()
                               .WithMessage("The one time password can`t be empty.");

            When(x => x.Otp != null, () =>
            {
                RuleFor(x => x.Otp).Must(otp => otp.Length == _otpLength)
                                   .WithMessage($"The one time password does not consist of {_otpLength} symbols.");
            });

        }
    }
}
