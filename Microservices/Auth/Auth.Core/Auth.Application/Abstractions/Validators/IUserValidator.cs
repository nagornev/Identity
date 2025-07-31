using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Validators
{
    public interface IUserValidator
    {
        bool Validate(User user);
    }
}
