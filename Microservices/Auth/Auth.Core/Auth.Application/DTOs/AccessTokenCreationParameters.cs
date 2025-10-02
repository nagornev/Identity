using Auth.Domain.Aggregates;

namespace Auth.Application.DTOs
{
    public class AccessTokenCreationParameters
    {
        public AccessTokenCreationParameters(User user,
                                             Session session,
                                             IReadOnlyCollection<Scope> scopes)
        {
            User = user;
            Session = session;
            Scopes = scopes;
        }

        public User User { get; }

        public Session Session { get; }

        public IReadOnlyCollection<Scope> Scopes { get; }
    }
}
