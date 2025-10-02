using System.IdentityModel.Tokens.Jwt;

namespace Auth.Security.Abstractions.Providers
{
    public interface IJwtClaimsProvider
    {
        Guid? GetKid(string token);

        JwtPayload? GetPayload(string token);
    }
}
