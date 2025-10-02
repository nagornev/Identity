using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Abstractions.Validators
{
    public interface IJwtSignatureValidator
    {
        bool Validate(string token, TokenValidationParameters parameters, out IReadOnlyDictionary<string, string> payload);

        Task<bool> ValidateAsync(string token, TokenValidationParameters parameters, CancellationToken cancellation = default);
    }
}
