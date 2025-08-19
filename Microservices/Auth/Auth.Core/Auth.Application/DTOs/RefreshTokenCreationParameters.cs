using Auth.Domain.Aggregates;

namespace Auth.Application.DTOs
{
    public class RefreshTokenCreationParameters
    {
        public RefreshTokenCreationParameters(User user,
                                              Session session)
        {
            User = user;
            Session = session;
        }

        public User User { get; }

        public Session Session { get; }
    }
}
