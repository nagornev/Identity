using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Tokens.Validators
{
    public abstract class JwtTokenValidator<TTokenOptions>: ITokenValidator
        where TTokenOptions : TokenOptions
    {
        private readonly ISecurityKeyProvider _securityKeyProvider;

        private readonly IJwtSignatureValidator _jwtSignatureValidator;

        private readonly TTokenOptions _tokenOptions;

        public JwtTokenValidator(ISecurityKeyProvider securityKeyProvider,
                                 IJwtSignatureValidator jwtValidator,
                                 IOptions<TTokenOptions> tokenOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _jwtSignatureValidator = jwtValidator;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<bool> ValidateAsync(string token, KeyPair keyPair, CancellationToken cancellation = default)
        {
            SecurityKey securityKey = _securityKeyProvider.Create(keyPair);

            return await _jwtSignatureValidator.ValidateAsync(token, new()
            {
                ValidateLifetime = true,

                ValidateIssuer = true,
                ValidIssuer = _tokenOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = _tokenOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

            }, cancellation) ;
        }
    }
}
