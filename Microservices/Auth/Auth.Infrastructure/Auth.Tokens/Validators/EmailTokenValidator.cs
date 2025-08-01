using Auth.Application.Abstractions.Storages;
using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;

namespace Auth.Tokens.Validators
{
    public class EmailTokenValidator : JwtTokenValidator<IEmailKeyStorage>, IEmailTokenValidator
    {
        public EmailTokenValidator(IJwtClaimsProvider jwtParser,
                                   IJwtSignatureValidator jwtValidator,
                                   IEmailKeyStorage keyStorage,
                                   ISecurityKeyProvider securityKeyProvider)
            : base(jwtParser, jwtValidator, keyStorage, securityKeyProvider)
        {
        }
    }
}
