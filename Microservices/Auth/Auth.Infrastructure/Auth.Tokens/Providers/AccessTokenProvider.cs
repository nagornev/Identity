using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Tokens.Providers
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        public AccessTokenProvider()
        {

        }

        public string Create(KeyPair accessKeyPair, User user, Session session)
        {
            throw new NotImplementedException();
        }
    }
}
