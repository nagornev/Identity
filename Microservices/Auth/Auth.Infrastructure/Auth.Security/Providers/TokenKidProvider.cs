using Auth.Application.Abstractions.Providers;
using Auth.Security.Abstractions.Providers;

namespace Auth.Security.Providers
{
    public class TokenKidProvider : ITokenKidProvider
    {
        private readonly IJwtClaimsProvider _jwtClaimsProvider;

        public TokenKidProvider(IJwtClaimsProvider jwtClaimsProvider)
        {
            _jwtClaimsProvider = jwtClaimsProvider;
        }

        public Guid? Get(string token)
        {
            return _jwtClaimsProvider.GetKid(token);
        }
    }
}
