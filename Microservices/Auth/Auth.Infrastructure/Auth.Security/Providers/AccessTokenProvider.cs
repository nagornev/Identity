using Auth.Application.Abstractions.Providers;
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
using System.Text;

namespace Auth.Security.Providers
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly ISecurityKeysProvider _securityKeyProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly ApplicationOptions _applicationOptions;

        private readonly AccessTokenOptions _tokenOptions;

        private readonly JwtSecurityTokenHandler _handler;

        public AccessTokenProvider(ISecurityKeysProvider securityKeyProvider,
                                   ITimeProvider timeProvider,
                                   IOptions<ApplicationOptions> applicationOptions,
                                   IOptions<AccessTokenOptions> tokenOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _timeProvider = timeProvider;
            _applicationOptions = applicationOptions.Value;
            _tokenOptions = tokenOptions.Value;

            _handler = new JwtSecurityTokenHandler();
        }

        public string Create(AccessTokenCreationParameters parameters, KeyPair keyPair)
        {
            SecurityKey securityKey = _securityKeyProvider.CreateSign(keyPair);

            Claim[] claims = CreateClaims(parameters.User, parameters.Session, parameters.Scopes);
            SigningCredentials credentials = CreateCredentials(securityKey, keyPair.Algorithm);
            JwtSecurityToken token = CreateToken(claims, credentials, parameters.Session);

            return _handler.WriteToken(token);
        }


        private Claim[] CreateClaims(User user, Session session, IReadOnlyCollection<Scope> scopes)
        {
            return
            [
                GetSubjectClaim(user),
                GetSessionClaim(session),
                GetJtiClaim(session),
                GetScopeClaim(scopes),
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

        private Claim GetScopeClaim(IReadOnlyCollection<Scope> scopes)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendJoin(' ', scopes.Select(x => x.GetName()));

            return new Claim(ClaimNames.Scope, builder.ToString());
        }

        private SigningCredentials CreateCredentials(SecurityKey securityKey, string algorithm)
        {
            return new SigningCredentials(securityKey, algorithm);
        }

        private JwtSecurityToken CreateToken(Claim[] claims, SigningCredentials credentials, Session session)
        {
            return new JwtSecurityToken(issuer: _applicationOptions.Issuer,
                                        audience: session.Audience.Value,
                                        claims: claims,
                                        signingCredentials: credentials,
                                        expires: _timeProvider.NowDateTime()
                                                              .AddSeconds(_tokenOptions.Lifetime));
        }


    }
}
