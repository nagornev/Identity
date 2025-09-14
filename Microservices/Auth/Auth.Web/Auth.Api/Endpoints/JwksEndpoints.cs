using Auth.Application.Abstractions.Storages;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Extensions;
using Carter;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Endpoints
{
    public class JwksEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/identity/jwks")
                           .AllowAnonymous();

            group.MapGet(string.Empty, GetJwks);
        }

        private static async Task<IResult> GetJwks(IAccessKeyStorage accessKeyStorage,
                                                   ISecurityKeysProvider securityKeyProvider,
                                                   CancellationToken cancellation = default)
        {
            JsonWebKeySet jwks = new JsonWebKeySet();
            IReadOnlyCollection<JsonWebKey> jsonWebKeys = await accessKeyStorage.GetJsonWebKeySetAsync(securityKeyProvider, cancellation);

            foreach (JsonWebKey jsonWebKey in jsonWebKeys)
            {
                jwks.Keys.Add(jsonWebKey);
            }

            return Results.Ok(jwks);
        }
    }
}
