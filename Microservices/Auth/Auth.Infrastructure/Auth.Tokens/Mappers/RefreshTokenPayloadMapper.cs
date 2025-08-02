using Auth.Application.Abstractions.Mappers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Providers;
using Auth.Tokens.Consts;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Mappers
{
    public class RefreshTokenPayloadMapper : IRefreshTokenPayloadMapper
    {
        private readonly IJwtClaimsProvider _jwtClaimsProvider;

        public RefreshTokenPayloadMapper(IJwtClaimsProvider jwtClaimsProvider)
        {
            _jwtClaimsProvider = jwtClaimsProvider;
        }
        public Task<RefreshTokenPayload> MapAsync(string token, CancellationToken cancellation = default)
        {
            Guid kid = _jwtClaimsProvider.GetKid(token);
            JwtPayload payload = _jwtClaimsProvider.GetPayload(token);

            RefreshTokenPayload refreshTokenDto = new RefreshTokenPayload(kid,
                                                                          Guid.Parse(payload.Sub),
                                                                          Guid.Parse(payload[ClaimNames.Session].ToString()!),
                                                                          Guid.Parse(payload.Jti),
                                                                          payload[ClaimNames.PublicKey].ToString()!);

            return Task.FromResult(refreshTokenDto);
        }
    }
}
