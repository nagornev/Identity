using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.Options;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Abstractions.Validators;
using Microsoft.Extensions.Options;

namespace Auth.Security.Validators
{
    public class RefreshTokenValidator : JwtTokenValidator, IRefreshTokenValidator
    {
        public RefreshTokenValidator(ISecurityKeysProvider securityKeyProvider,
                                     IJwtSignatureValidator jwtValidator,
                                     IOptions<ApplicationOptions> applicationOptions)
            : base(securityKeyProvider, jwtValidator, applicationOptions)
        {
        }
    }
}
