using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;

namespace Auth.Tokens.Validators
{
    public class AccessTokenValidator : JwtTokenValidator<IAccessKeyStorage>, IAccessTokenValidator
    {
        public AccessTokenValidator(IJwtClaimsProvider jwtParser,
                                    IJwtSignatureValidator jwtValidator,
                                    IAccessKeyStorage keyStorage,
                                    ISecurityKeyProvider securityKeyProvider)
            : base(jwtParser, jwtValidator, keyStorage, securityKeyProvider)
        {
        }
    }
}
