using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Abstractions.Validators;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Validators
{
    public abstract class JwtTokenValidator : ITokenValidator
    {
        private readonly ISecurityKeysProvider _securityKeyProvider;

        private readonly IJwtSignatureValidator _jwtSignatureValidator;

        private readonly ApplicationOptions _applicationOptions;

        public JwtTokenValidator(ISecurityKeysProvider securityKeyProvider,
                                 IJwtSignatureValidator jwtValidator,
                                 IOptions<ApplicationOptions> applicationOptions)
        {
            _securityKeyProvider = securityKeyProvider;
            _jwtSignatureValidator = jwtValidator;
            _applicationOptions = applicationOptions.Value;
        }

        public bool Validate(string token, KeyPair keyPair, out IReadOnlyDictionary<string, string> payload)
        {
            SecurityKey securityKey = _securityKeyProvider.CreateVerify(keyPair);

            return _jwtSignatureValidator.Validate(token, new()
            {
                ValidateLifetime = true,

                ValidateIssuer = true,
                ValidIssuer = _applicationOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = _applicationOptions.Issuer,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
            },
            out payload);
        }
    }
}
