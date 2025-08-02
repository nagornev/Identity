using Auth.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
