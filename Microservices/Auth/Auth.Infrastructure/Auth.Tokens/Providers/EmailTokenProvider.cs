using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using Auth.Keys.Abstractions.Providers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Tokens.Providers
{
    public class EmailTokenProvider : IEmailTokenProvider
    {
        private readonly ISecurityKeyProvider _securityKeyProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly EmailTokenOptions _tokenOptions;

        private readonly JwtSecurityTokenHandler _handler;

        public EmailTokenProvider(ISecurityKeyProvider securityKeyProvider,
                                  ITimeProvider timeProvider,
                                  IOptions<EmailTokenOptions> tokenOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _timeProvider = timeProvider;
            _tokenOptions = tokenOptions.Value;

            _handler = new JwtSecurityTokenHandler();
        }

        public string Create(EmailTokenCreationParameters parameters, KeyPair keyPair)
        {
            SecurityKey securityKey = _securityKeyProvider.Create(keyPair);

            Claim[] claims = CreateClaims(parameters.UserId);
            SigningCredentials credentials = CreateCredentials(securityKey, keyPair.Algorithm);
            JwtSecurityToken token = CreateToken(claims, credentials);

            return _handler.WriteToken(token);
        }

        private Claim[] CreateClaims(Guid userId)
        {
            return
            [
                GetSubjectClaim(userId)
            ];
        }

        private Claim GetSubjectClaim(Guid userId)
        {
            return new Claim(JwtRegisteredClaimNames.Sub, userId.ToString());
        }


        private SigningCredentials CreateCredentials(SecurityKey securityKey, string algorithm)
        {
            return new SigningCredentials(securityKey, algorithm);
        }

        private JwtSecurityToken CreateToken(Claim[] claims, SigningCredentials credentials)
        {
            return new JwtSecurityToken(issuer: _tokenOptions.Issuer,
                                        audience: _tokenOptions.Audience,
                                        claims: claims,
                                        signingCredentials: credentials,
                                        expires: _timeProvider.NowDateTime()
                                                              .AddSeconds(_tokenOptions.Lifetime));
        }
    }
}
