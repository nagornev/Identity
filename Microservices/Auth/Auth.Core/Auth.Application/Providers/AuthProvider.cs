using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Providers
{
    public class AuthProvider : IAuthTokensProvider
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        public AuthProvider(IAccessTokenProvider accessTokenProvider,
                            IRefreshTokenProvider refreshTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
        }

        public DTOs.AuthTokens Create(KeyPair accessKeyPair, KeyPair refreshKeyPair, User user, Session session, string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}
