using Auth.Application.Abstractions.Mappers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Providers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Mappers
{
    public class EmailTokenMapper : IEmailTokenMapper
    {
        private readonly IJwtClaimsProvider _jwtParser;

        public EmailTokenMapper(IJwtClaimsProvider jwtParser)
        {
            _jwtParser = jwtParser;
        }

        public Task<EmailToken> MapAsync(string token, CancellationToken cancellation = default)
        {
            JwtPayload payload = _jwtParser.GetPayload(token);

            EmailToken emailTokenDto = new EmailToken(Guid.Parse(payload.Sub));

            return Task.FromResult(emailTokenDto);
        }
    }
}
