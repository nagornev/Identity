using Auth.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTOs
{
    public class RefreshTokenCreationParameters
    {
        public RefreshTokenCreationParameters(User user,
                                              Session session,
                                              string publicKey)
        {
            User = user;
            Session = session;
            PublicKey = publicKey;
        }

        public User User { get; }

        public Session Session { get; }

        public string PublicKey { get; }
    }
}
