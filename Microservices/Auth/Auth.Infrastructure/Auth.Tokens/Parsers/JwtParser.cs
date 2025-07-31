using Auth.Tokens.Abstractions.Parsers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Parsers
{
    public class JwtParser : IJwtParser
    {
        public Guid GetKid(string token)
        {
            JwtSecurityToken jwt = GetJwt(token);

            return Guid.Parse(jwt.Header.Kid);
        }

        public JwtPayload GetPayload(string token)
        {
            JwtSecurityToken jwtSecurityToken = GetJwt(token);

            return jwtSecurityToken.Payload;
        }

        private JwtSecurityToken GetJwt(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }
    }
}
