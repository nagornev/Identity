using Auth.Application.Abstractions.Mappers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Mappers
{
    public class RefreshTokenMapper : IRefreshTokenMapper
    {
        private readonly IJwtClaimsProvider _jwtClaimsProvider;

        public RefreshTokenMapper(IJwtClaimsProvider jwtClaimsProvider)
        {
            _jwtClaimsProvider = jwtClaimsProvider;
        }
        public Task<RefreshToken> MapAsync(string token, CancellationToken cancellation = default)
        {
            Guid kid = _jwtClaimsProvider.GetKid(token);
            JwtPayload payload = _jwtClaimsProvider.GetPayload(token);

            RefreshToken refreshTokenDto = new RefreshToken(kid,
                                                                  Guid.Parse(payload.Sub),
                                                                  Guid.Parse(payload[JwtClaims.Session].ToString()!),
                                                                  Guid.Parse(payload.Jti),
                                                                  payload[JwtClaims.PublicKey].ToString()!);

            return Task.FromResult(refreshTokenDto);
        }
    }
}
