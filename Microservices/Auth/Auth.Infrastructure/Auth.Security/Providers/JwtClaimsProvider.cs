using Auth.Security.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Security.Providers
{
    public class JwtClaimsProvider : IJwtClaimsProvider
    {
        public Guid GetKid(string token)
        {
            JwtSecurityToken jwt = GetJwt(token);

            return Guid.Parse(jwt.Header.Kid);
        }

        public JwtPayload GetPayload(string token)
        {
            JwtSecurityToken jwt = GetJwt(token);

            return jwt.Payload;
        }

        private JwtSecurityToken GetJwt(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }
    }
}
