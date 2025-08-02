using Auth.Application.Abstractions.Validators.Tokens;
using Auth.Application.Options;
using Auth.Keys.Abstractions.Providers;
using Auth.Tokens.Abstractions.Validators;
using Microsoft.Extensions.Options;

namespace Auth.Tokens.Validators
{
    public class EmailTokenValidator : JwtTokenValidator, IEmailTokenValidator
    {
        public EmailTokenValidator(ISecurityKeyProvider securityKeyProvider,
                                   IJwtSignatureValidator jwtValidator,
                                   IOptions<ApplicationOptions> applicationOptions) 
            : base(securityKeyProvider, jwtValidator, applicationOptions)
        {
        }
    }
}
