using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Keys.Abstractions.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Keys.Extensions
{
    public static class KeyStorageReaderExtensions
    {
        public static async Task<IReadOnlyCollection<JsonWebKey>> GetJsonWebKeySetAsync(this IKeyStorageReader keyStorage, ISecurityKeyProvider securityKeyProvider, CancellationToken cancellation = default)
        {
            IReadOnlyCollection<KeyPair> keyPairs = await keyStorage.GetKeyPairsAsync(cancellation);

            return keyPairs.Select(securityKeyProvider.Create)
                           .Select(JsonWebKeyConverter.ConvertFromSecurityKey)
                           .ToArray();
        }
    }
}
