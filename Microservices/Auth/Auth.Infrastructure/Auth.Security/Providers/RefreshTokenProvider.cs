using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Consts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Security.Providers
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        private readonly ISecurityKeysProvider _securityKeyProvider;

        private readonly ApplicationOptions _applicationOptions;

        private readonly JwtSecurityTokenHandler _handler;

        public RefreshTokenProvider(ISecurityKeysProvider securityKeyProvider,
                                    IOptions<ApplicationOptions> applicationOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _applicationOptions = applicationOptions.Value;

            _handler = new JwtSecurityTokenHandler();
        }

        public string Create(RefreshTokenCreationParameters parameters, KeyPair keyPair)
        {
            SecurityKey securityKey = _securityKeyProvider.CreateSign(keyPair);

            Claim[] claims = CreateClaims(parameters.User, parameters.Session);
            SigningCredentials credentials = CreateCredentials(securityKey, keyPair.Algorithm);
            JwtSecurityToken token = CreateToken(claims, credentials, parameters.Session);

            return _handler.WriteToken(token);
        }

        private Claim[] CreateClaims(User user, Session session)
        {
            return
            [
                GetSubjectClaim(user),
                GetSessionClaim(session),
                GetJtiClaim(session),
            ];
        }

        private Claim GetSubjectClaim(User user)
        {
            return new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
        }

        private Claim GetSessionClaim(Session session)
        {
            return new Claim(ClaimNames.Session, session.Id.ToString());
        }

        private Claim GetJtiClaim(Session session)
        {
            return new Claim(JwtRegisteredClaimNames.Jti, session.Version.ToString());
        }

        private SigningCredentials CreateCredentials(SecurityKey securityKey, string algorithm)
        {
            return new SigningCredentials(securityKey, algorithm);
        }

        private JwtSecurityToken CreateToken(Claim[] claims, SigningCredentials credentials, Session session)
        {
            return new JwtSecurityToken(issuer: _applicationOptions.Issuer,
                                        audience: _applicationOptions.Issuer,
                                        claims: claims,
                                        signingCredentials: credentials,
                                        expires: DateTimeOffset.FromUnixTimeSeconds(session.ExpiresAt).DateTime);
        }


    }
}
