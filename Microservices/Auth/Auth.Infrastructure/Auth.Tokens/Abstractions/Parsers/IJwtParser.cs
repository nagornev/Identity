using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Abstractions.Parsers
{
    public interface IJwtParser
    {
        Guid GetKid(string token);

        JwtPayload GetPayload(string token);
    }
}
