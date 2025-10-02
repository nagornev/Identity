using Auth.Security.Consts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value ??
                                                                    throw new InvalidOperationException());
        }

        public static Guid GetSessionId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimNames.Session)?.Value ??
                                                                    throw new InvalidOperationException());
        }
    }
}
