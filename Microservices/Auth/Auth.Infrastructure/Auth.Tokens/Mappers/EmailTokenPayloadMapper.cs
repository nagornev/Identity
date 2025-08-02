using Auth.Application.Abstractions.Mappers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Mappers
{
    public class EmailTokenPayloadMapper : IEmailTokenPayloadMapper
    {
        private readonly IJwtClaimsProvider _jwtClaimsProvider;

        public EmailTokenPayloadMapper(IJwtClaimsProvider jwtClaimsProvider)
        {
            _jwtClaimsProvider = jwtClaimsProvider;
        }

        public Task<EmailTokenPayload> MapAsync(string token, CancellationToken cancellation = default)
        {
            Guid kid = _jwtClaimsProvider.GetKid(token);
            JwtPayload payload = _jwtClaimsProvider.GetPayload(token);

            EmailTokenPayload emailToken = new EmailTokenPayload(kid,
                                                                 Guid.Parse(payload.Sub));

            return Task.FromResult(emailToken);
        }
    }
}
