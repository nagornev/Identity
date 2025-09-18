using Auth.Domain.ValueObjects;
using FluentValidation;

namespace Auth.Api.Contracts
{
    public class PersonNameChangeContractValidator : AbstractValidator<PersonNameChangeContract>
    {
        public PersonNameChangeContractValidator()
        {
            RuleFor(x => x.NewPersonName).NotNull()
                                         .WithMessage("The new person name can`t be null.");
            RuleFor(x => x.NewPersonName).NotEmpty()
                                         .WithMessage("The new person name can`t be empty.");


            When(x => x.NewPersonName != null, () =>
            {
                RuleFor(x => x.NewPersonName).Must(name => !string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
                                             .WithMessage("The file name can`t be null or empty.")
                                             .Must(name => name.Length < PersonName.MaximumLength && name.Length >= PersonName.MinimumLength)
                                             .WithMessage($"The file lenght can`t be less than {PersonName.MinimumLength} and more than {PersonName.MaximumLength} bytes.");
            });
        }
    }
}
