using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Validators;
using Auth.Domain.Aggregates;

namespace Auth.Application.Validators
{
    public class SessionValidator : ISessionValidator
    {
        private readonly ITimeProvider _timeProvider;

        public SessionValidator(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public bool Validate(Session session)
        {
            return session.IsValidAt(_timeProvider.NowUnix());
        }

        public bool ValidateWithoutActive(Session session)
        {
            return !session.Closed &&
                   !session.Deleted &&
                   !session.Revoked &&
                   session.ExpiresAt > _timeProvider.NowUnix();
        }
    }
}
