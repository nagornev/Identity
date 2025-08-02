using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Providers.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Consts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Auth.Tokens.Providers
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        private readonly ISecurityKeyProvider _securityKeyProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly RefreshTokenOptions _tokenOptions;

        private readonly JwtSecurityTokenHandler _handler;

        public RefreshTokenProvider(ISecurityKeyProvider securityKeyProvider,
                                    ITimeProvider timeProvider,
                                    IOptions<RefreshTokenOptions> tokenOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _timeProvider = timeProvider;
            _tokenOptions = tokenOptions.Value;

            _handler = new JwtSecurityTokenHandler();
        }

        public string Create(RefreshTokenCreationParameters parameters, KeyPair keyPair)
        {
            SecurityKey securityKey = _securityKeyProvider.Create(keyPair);

            Claim[] claims = CreateClaims(parameters.User, parameters.Session, parameters.PublicKey);
            SigningCredentials credentials = CreateCredentials(securityKey, keyPair.Algorithm);
            JwtSecurityToken token = CreateToken(claims, credentials);

            return _handler.WriteToken(token);
        }

        private Claim[] CreateClaims(User user, Session session, string publicKey)
        {
            return
            [
                GetSubjectClaim(user),
                GetSessionClaim(session),
                GetJtiClaim(session),
                GetPublicKeyClaim(publicKey)
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

        private Claim GetPublicKeyClaim(string publicKey)
        {
            return new Claim(ClaimNames.PublicKey, publicKey);
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
