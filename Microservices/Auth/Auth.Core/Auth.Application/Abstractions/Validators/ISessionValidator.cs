using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Validators
{
    public interface ISessionValidator
    {
        bool ValidateWithoutActive(Session session);

        bool Validate(Session session);
    }
}
