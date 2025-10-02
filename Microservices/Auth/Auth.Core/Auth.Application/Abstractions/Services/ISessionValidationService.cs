using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface ISessionValidationService
    {
        void Validate(Session session);

        void ValidateWithoutActive(Session session);

        void ValidateVersion(Session session, Guid version);
    }
}
