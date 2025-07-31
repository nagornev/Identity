using Microsoft.IdentityModel.Tokens;

namespace Auth.Tokens.Abstractions.Validators
{
    public interface IJwtValidator
    {
        bool Validate(string token, TokenValidationParameters parameters);

        Task<bool> ValidateAsync(string token, TokenValidationParameters parameters, CancellationToken cancellation = default);
    }
}
