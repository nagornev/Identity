using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Consts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Security.Providers
{
    public class ChannelTokenProvider : IChannelTokenProvider
    {
        private readonly ISecurityKeysProvider _securityKeyProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly ApplicationOptions _applicationOptions;

        private readonly ChannelTokenOptions _tokenOptions;

        private readonly JwtSecurityTokenHandler _handler;

        public ChannelTokenProvider(ISecurityKeysProvider securityKeyProvider,
                                    ITimeProvider timeProvider,
                                    IOptions<ApplicationOptions> applicationOptions,
                                    IOptions<ChannelTokenOptions> tokenOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _timeProvider = timeProvider;
            _applicationOptions = applicationOptions.Value;
            _tokenOptions = tokenOptions.Value;

            _handler = new JwtSecurityTokenHandler();
        }

        public string Create(ChannelTokenCreationParameters parameters, KeyPair keyPair)
        {
            SecurityKey securityKey = _securityKeyProvider.CreateSign(keyPair);

            Claim[] claims = CreateClaims(parameters);
            SigningCredentials credentials = CreateCredentials(securityKey, keyPair.Algorithm);
            JwtSecurityToken token = CreateToken(claims, credentials);

            return _handler.WriteToken(token);
        }

        private Claim[] CreateClaims(ChannelTokenCreationParameters parameters)
        {
            return
            [
                GetSubjectClaim(parameters.UserId),
                GetTagClaim(parameters.Tag),
                GetChannelClaim(parameters.Channel)
            ];
        }

        private Claim GetSubjectClaim(Guid userId)
        {
            return new Claim(JwtRegisteredClaimNames.Sub, userId.ToString());
        }

        private Claim GetTagClaim(string tag)
        {
            return new Claim(ClaimNames.ChannelTag, tag);
        }

        private Claim GetChannelClaim(string channel)
        {
            return new Claim(ClaimNames.Channel, channel);
        }

        private SigningCredentials CreateCredentials(SecurityKey securityKey, string algorithm)
        {
            return new SigningCredentials(securityKey, algorithm);
        }

        private JwtSecurityToken CreateToken(Claim[] claims, SigningCredentials credentials)
        {
            return new JwtSecurityToken(issuer: _applicationOptions.Issuer,
                                        audience: _applicationOptions.Issuer,
                                        claims: claims,
                                        signingCredentials: credentials,
                                        expires: _timeProvider.NowDateTime()
                                                              .AddSeconds(_tokenOptions.Lifetime));
        }
    }
}
