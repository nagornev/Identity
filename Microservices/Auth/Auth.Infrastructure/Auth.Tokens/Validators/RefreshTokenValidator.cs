using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.Options;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;
using Microsoft.Extensions.Options;

namespace Auth.Tokens.Validators
{
    public class RefreshTokenValidator : JwtTokenValidator, IRefreshTokenValidator
    {
        public RefreshTokenValidator(ISecurityKeyProvider securityKeyProvider,
                                     IJwtSignatureValidator jwtValidator,
                                     IOptions<ApplicationOptions> applicationOptions)
            : base(securityKeyProvider, jwtValidator, applicationOptions)
        {
        }
    }
}
