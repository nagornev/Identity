using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Tokens.Extensions
{
    internal static class JwtSecurityTokenHandlerExtensions
    {
        public static bool Validate(this JwtSecurityTokenHandler handler,
                                    string token,
                                    TokenValidationParameters parameters)
        {
            try
            {
                _ = handler.ValidateToken(token, parameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> ValidateAsync(this JwtSecurityTokenHandler handler,
                                                     string token,
                                                     TokenValidationParameters parameters,
                                                     CancellationToken cancellation = default)
        {
            TokenValidationResult validationResult = await handler.ValidateTokenAsync(token, parameters);
            return validationResult.IsValid;
        }
    }
}
