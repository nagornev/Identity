using Auth.Security.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Security.Providers
{
    public class JwtClaimsProvider : IJwtClaimsProvider
    {
        public Guid? GetKid(string token)
        {
            JwtSecurityToken? jwt = GetJwt(token);

            return jwt!= null?
                    Guid.Parse(jwt.Header.Kid):
                    null;
        }

        public JwtPayload? GetPayload(string token)
        {
            JwtSecurityToken? jwt = GetJwt(token);

            return jwt != null?
                     jwt.Payload:
                     null;
        }

        private JwtSecurityToken? GetJwt(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            try
            {
                return handler.ReadJwtToken(token);
            }
            catch(ArgumentException)
            {
                return null;
            }
        }
    }
}
