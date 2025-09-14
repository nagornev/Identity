using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Security.Abstractions.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Security.Extensions
{
    public static class KeyStorageReaderExtensions
    {
        public static async Task<IReadOnlyCollection<JsonWebKey>> GetJsonWebKeySetAsync(this IKeyStorageReader keyStorage, ISecurityKeysProvider securityKeyProvider, CancellationToken cancellation = default)
        {
            IReadOnlyCollection<KeyPair> keyPairs = await keyStorage.GetKeyPairsAsync(cancellation);

            return keyPairs.Select(securityKeyProvider.CreateVerify)
                           .Select(JsonWebKeyConverter.ConvertFromSecurityKey)
                           .ToArray();
        }

        public static async Task<IReadOnlyCollection<SecurityKey>> GetSecurityKeysAsync(this IKeyStorageReader keyStorage, ISecurityKeysProvider securityKeyProvider, CancellationToken cancellation = default)
        {
            IReadOnlyCollection<KeyPair> keyPairs = await keyStorage.GetKeyPairsAsync(cancellation);

            return keyPairs.Select(securityKeyProvider.CreateVerify)
                           .ToArray();
        }
    }
}
