using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;

namespace Auth.Tokens.Validators
{
    public class RefreshTokenValidator : JwtTokenValidator<IRefreshKeyStorage>, IRefreshTokenValidator
    {
        public RefreshTokenValidator(IJwtClaimsProvider jwtParser,
                                     IJwtSignatureValidator jwtValidator,
                                     IRefreshKeyStorage keyStorage,
                                     ISecurityKeyProvider securityKeyProvider)
            : base(jwtParser, jwtValidator, keyStorage, securityKeyProvider)
        {
        }
    }
}
