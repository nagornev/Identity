using Auth.Security.Abstractions.Validators;
using Auth.Security.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Security.Validators
{
    public class JwtSignatureValidator : IJwtSignatureValidator
    {
        private readonly JwtSecurityTokenHandler _handler;

        public JwtSignatureValidator()
        {
            _handler = new JwtSecurityTokenHandler();
        }

        public bool Validate(string token, TokenValidationParameters parameters, out IReadOnlyDictionary<string, string> payload)
        {
            return _handler.Validate(token, parameters, out payload);
        }

        public async Task<bool> ValidateAsync(string token, TokenValidationParameters parameters, CancellationToken cancellation = default)
        {
            return await _handler.ValidateAsync(token, parameters, cancellation);
        }
    }
}
