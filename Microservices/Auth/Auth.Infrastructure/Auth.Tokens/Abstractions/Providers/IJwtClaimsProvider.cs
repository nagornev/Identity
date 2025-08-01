using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Abstractions.Providers
{
    public interface IJwtClaimsProvider
    {
        Guid GetKid(string token);

        JwtPayload GetPayload(string token);
    }
}
