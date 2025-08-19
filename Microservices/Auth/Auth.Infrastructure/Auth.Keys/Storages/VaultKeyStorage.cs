using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Keys.Abstractions.Providers;
using Auth.Keys.Options;
using Microsoft.Extensions.Options;
using VaultSharp;

namespace Auth.Keys.Storages
{
    public class VaultKeyStorage<TVaultStorageClientProvider, TVaultKeyStorageOptionsType> : KeyStorage<TVaultKeyStorageOptionsType>, IKeyStorage
        where TVaultStorageClientProvider : IVaultStorageClientProvider
        where TVaultKeyStorageOptionsType : VaultKeyStorageOptions
    {
        private readonly IVaultClient _vaultClient;

        private readonly TVaultKeyStorageOptionsType _keyStorageOptions;

        public VaultKeyStorage(TVaultStorageClientProvider vaultStorageClientProvider,
                               IOptions<TVaultKeyStorageOptionsType> keyStorageOptions)
            : base(keyStorageOptions)
        {
            _vaultClient = vaultStorageClientProvider.Create();
            _keyStorageOptions = keyStorageOptions.Value;
        }

        public override async Task SetPrimaryAsync(KeyPair keyPair, CancellationToken cancellation = default)
        {
            KeyPair previewPrimaryKey = await GetPrimaryAsync(cancellation);
            var previewPrimaryKeyDictionary = KeyPairToDictionary(previewPrimaryKey);

            await _vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(
                    path: $"{_keyStorageOptions.BasePath}/{previewPrimaryKey.Kid}",
                    data: previewPrimaryKeyDictionary);

            var nextPrimaryKey = KeyPairToDictionary(keyPair);

            await _vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(
                    path: $"{_keyStorageOptions.BasePath}/{_keyStorageOptions.PrimaryKey}",
                    data: nextPrimaryKey);
        }

        public override async Task DeleteKeyPairAsync(Guid kid, CancellationToken cancellation = default)
        {
            await _vaultClient.V1.Secrets.KeyValue.V2.DeleteSecretAsync(path: $"{_keyStorageOptions.BasePath}/{kid}");
        }

        public override async Task<KeyPair> GetKeyPairAsync(Guid kid, CancellationToken cancellation = default)
        {
            KeyPair primaryKey = await GetPrimaryAsync(cancellation);

            if (primaryKey.Kid == kid)
                return primaryKey;

            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync($"{_keyStorageOptions.BasePath}/{kid}");
            return DictionaryToKeyPair(secret.Data.Data);
        }

        public override async Task<IReadOnlyCollection<KeyPair>> GetKeyPairsAsync(CancellationToken cancellation = default)
        {
            var paths = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync($"{_keyStorageOptions.BasePath}/");

            var keyPairs = await Task.WhenAll(paths.Data.Keys
                                                        .Select(x => x.TrimEnd('/'))
                                                        .Select(async key =>
                                                        {
                                                            if (key == _keyStorageOptions.PrimaryKey)
                                                                return await GetPrimaryAsync(cancellation);

                                                            return Guid.TryParse(key, out var kid) ?
                                                                   await GetKeyPairAsync(kid, cancellation) :
                                                                   null;
                                                        }));

            return keyPairs.Where(x => x != null)
                           .DistinctBy(x => x!.Kid)
                           .ToArray()!;
        }

        public override async Task<KeyPair> GetPrimaryAsync(CancellationToken cancellation = default)
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync($"{_keyStorageOptions.BasePath}/{_keyStorageOptions.PrimaryKey}");
            return DictionaryToKeyPair(secret.Data.Data);
        }

        private IDictionary<string, object> KeyPairToDictionary(KeyPair keyPair) => new Dictionary<string, object>
        {
            [Properties.Kid] = keyPair.Kid.ToString(),
            [Properties.Algorithm] = keyPair.Algorithm,
            [Properties.PublicKey] = Convert.ToBase64String(keyPair.PublicKey),
            [Properties.PrivateKey] = Convert.ToBase64String(keyPair.PrivateKey),
            [Properties.CreatedAt] = keyPair.CreatedAt,
            [Properties.ExpiresAt] = keyPair.ExpiresAt
        };

        private KeyPair DictionaryToKeyPair(IDictionary<string, object> dictionary) =>
            new KeyPair(Guid.Parse(dictionary[Properties.Kid].ToString()!),
                        dictionary[Properties.Algorithm].ToString()!,
                        Convert.FromBase64String(dictionary[Properties.PublicKey].ToString()!),
                        Convert.FromBase64String(dictionary[Properties.PrivateKey].ToString()!),
                        long.Parse(dictionary[Properties.CreatedAt].ToString()!),
                        long.Parse(dictionary[Properties.ExpiresAt].ToString()!));

        private static class Properties
        {
            public const string Kid = "kid";

            public const string Algorithm = "alg";

            public const string PublicKey = "pbk";

            public const string PrivateKey = "prk";

            public const string CreatedAt = "cre";

            public const string ExpiresAt = "exp";
        }
    }
}
