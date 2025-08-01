using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Domain.Aggregates;

namespace Auth.Application.Providers
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        public AuthProvider(IAccessTokenProvider accessTokenProvider,
                            IRefreshTokenProvider refreshTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
            _refreshTokenProvider = refreshTokenProvider;
        }

        public AuthDto Create(KeyPairDto accessKeyPair, KeyPairDto refreshKeyPair, User user, Session session, string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}
