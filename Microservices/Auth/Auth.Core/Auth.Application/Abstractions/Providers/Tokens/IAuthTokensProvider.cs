using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Providers.Tokens
{
    public interface IAuthTokensProvider
    {
        AuthTokens Create(KeyPair accessKeyPair, KeyPair refreshKeyPair, User user, Session session, string publicKey);
    }
}
