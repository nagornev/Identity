using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Validators;
using Auth.Application.Exceptions.Applications.Security;
using Auth.Application.Exceptions.Applications.Sessions;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class SessionValidationService : ISessionValidationService
    {
        private readonly ISessionValidator _sessionValidator;

        public SessionValidationService(ISessionValidator sessionValidator)
        {
            _sessionValidator = sessionValidator;
        }

        public void Validate(Session session)
        {
            if (!_sessionValidator.Validate(session))
                throw new SessionInvalidApplicationException(session.Id);
        }

        public void ValidateWithoutActive(Session session)
        {
            if (!_sessionValidator.ValidateWithoutActive(session))
                throw new SessionInvalidApplicationException(session.Id);
        }

        public void ValidateVersion(Session session, Guid version)
        {
            if (session.Version != version)
                throw new VersionInvalidApplicationException();
        }
    }
}
