using Auth.Application.Abstractions.Mappers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Providers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Tokens.Mappers
{
    public class RefreshTokenMapper : IRefreshTokenMapper
    {
        private readonly IJwtClaimsProvider _jwtClaimsProvider;

        public RefreshTokenMapper(IJwtClaimsProvider jwtClaimsProvider)
        {
            _jwtClaimsProvider = jwtClaimsProvider;
        }
        public Task<RefreshTokenDto> MapAsync(string token, CancellationToken cancellation = default)
        {
            Guid kid = _jwtClaimsProvider.GetKid(token);
            JwtPayload payload = _jwtClaimsProvider.GetPayload(token);

            RefreshTokenDto refreshTokenDto = new RefreshTokenDto(kid,
                                                                  Guid.Parse(payload.Sub),
                                                                  Guid.Parse(payload[JwtClaims.Session].ToString()!),
                                                                  Guid.Parse(payload.Jti),
                                                                  payload[JwtClaims.PublicKey].ToString()!);

            return Task.FromResult(refreshTokenDto);
        }
    }
}
