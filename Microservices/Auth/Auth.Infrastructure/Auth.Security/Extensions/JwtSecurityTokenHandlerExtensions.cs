using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Security.Extensions
{
    internal static class JwtSecurityTokenHandlerExtensions
    {
        public static bool Validate(this JwtSecurityTokenHandler handler,
                                    string token,
                                    TokenValidationParameters parameters,
                                    out IReadOnlyDictionary<string, string> payload)
        {
            try
            {
                ClaimsPrincipal principal = handler.ValidateToken(token, parameters, out _);

                Dictionary<string, string> claims = new Dictionary<string, string>(principal.Claims.Count());
                foreach (Claim claim in principal.Claims)
                {
                    claims.Add(claim.Type, claim.Value);
                }
                payload = claims;

                return true;
            }
            catch
            {
                payload = System.Collections.Immutable.ImmutableDictionary<string, string>.Empty;
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
