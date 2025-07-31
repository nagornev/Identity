using Auth.Application.Abstractions.Mappers;
using Auth.Application.DTOs;
using Auth.Tokens.Abstractions.Parsers;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Mappers
{
    public class EmailTokenMapper : IEmailTokenMapper
    {
        private readonly IJwtParser _jwtParser;

        public EmailTokenMapper(IJwtParser jwtParser)
        {
            _jwtParser = jwtParser;
        }

        public Task<EmailTokenDto> MapAsync(string token, CancellationToken cancellation = default)
        {
            Guid kid = _jwtParser.GetKid(token);
            JwtPayload payload = _jwtParser.GetPayload(token);

            EmailTokenDto emailTokenDto = new EmailTokenDto(kid,
                                                            Guid.Parse(payload[JwtClaims.Subject].ToString()!),
                                                            payload[JwtClaims.EmailAddress].ToString()!);

            return Task.FromResult(emailTokenDto);
        }
    }
}
