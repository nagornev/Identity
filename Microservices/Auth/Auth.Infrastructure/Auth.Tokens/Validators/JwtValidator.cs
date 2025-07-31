using Auth.Tokens.Abstractions.Validators;
using Auth.Tokens.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Validators
{
    public class JwtValidator : IJwtValidator
    {
        private readonly JwtSecurityTokenHandler _handler;

        public JwtValidator()
        {
            _handler = new JwtSecurityTokenHandler();
        }

        public bool Validate(string token, TokenValidationParameters parameters)
        {
            return _handler.Validate(token, parameters);
        }

        public async Task<bool> ValidateAsync(string token, TokenValidationParameters parameters, CancellationToken cancellation = default)
        {
            return await _handler.ValidateAsync(token, parameters, cancellation);
        }
    }
}
