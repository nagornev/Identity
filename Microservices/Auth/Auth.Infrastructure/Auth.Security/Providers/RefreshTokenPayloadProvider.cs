using Auth.Application.Abstractions.Providers;
using Auth.Application.DTOs;
using Auth.Security.Consts;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Security.Providers
{
    public class RefreshTokenPayloadProvider : IRefreshTokenPayloadProvider
    {

        public RefreshTokenPayload GetPayload(IReadOnlyDictionary<string, string> claims)
        {
            return new RefreshTokenPayload(Guid.Parse(claims[JwtRegisteredClaimNames.Sub]),
                                           Guid.Parse(claims[ClaimNames.Session]),
                                           Guid.Parse(claims[JwtRegisteredClaimNames.Jti]));
        }
    }
}

