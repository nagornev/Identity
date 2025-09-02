using Auth.Application.Abstractions.Validators;
using Auth.Domain.Aggregates;

namespace Auth.Application.Validators
{
    public class UserValidator : IUserValidator
    {
        public bool Validate(User user)
        {
            return user.Activated;
        }
    }
}
