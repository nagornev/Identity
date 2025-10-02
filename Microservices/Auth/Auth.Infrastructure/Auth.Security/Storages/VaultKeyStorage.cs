using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Security.Abstractions.Providers;
using Auth.Security.Options;
using Microsoft.Extensions.Options;
using System.Net;
using VaultSharp;
using VaultSharp.Core;

namespace Auth.Security.Storages
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

        public override async Task<IReadOnlyCollection<KeyPair>> GetKeyPairsAsync(CancellationToken cancellation = default)
        {
            return await GetKeyPairsAsync($"{_keyStorageOptions.BasePath}");
        }

        public override async Task<KeyPair> GetKeyPairAsync(Guid kid, CancellationToken cancellation = default)
        {
            KeyPair primaryKey = await GetPrimaryAsync(cancellation);

            return primaryKey.Kid == kid ?
                    primaryKey :
                    await GetKeyPairAsync($"{_keyStorageOptions.BasePath}/{kid}");
        }

        public override async Task<KeyPair> GetPrimaryAsync(CancellationToken cancellation = default)
        {
            return await GetKeyPairAsync($"{_keyStorageOptions.BasePath}/{_keyStorageOptions.PrimaryKey}");
        }

        public override async Task SetPrimaryAsync(KeyPair keyPair, CancellationToken cancellation = default)
        {
            KeyPair previewPrimaryKey = await GetPrimaryAsync(cancellation);

            if (previewPrimaryKey != null)
            {
                var previewPrimaryKeyDictionary = KeyPairToDictionary(previewPrimaryKey);
                await SetKeyPairAsync(previewPrimaryKeyDictionary, $"{_keyStorageOptions.BasePath}/{previewPrimaryKey.Kid}");
            }

            var nextPrimaryKey = KeyPairToDictionary(keyPair);
            await SetKeyPairAsync(nextPrimaryKey, $"{_keyStorageOptions.BasePath}/{_keyStorageOptions.PrimaryKey}");
        }

        public override async Task DeleteKeyPairAsync(Guid kid, CancellationToken cancellation = default)
        {
            try
            {
                await _vaultClient.V1.Secrets.KeyValue.V2.DeleteSecretAsync(path: $"{_keyStorageOptions.BasePath}/{kid}");
            }
            catch (VaultApiException exception) when (exception.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return;
            }
        }

        private async Task<KeyPair?> GetKeyPairAsync(string path, CancellationToken cancellation = default)
        {
            try
            {
                var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path, mountPoint: _keyStorageOptions.MountPoint);
                return DictionaryToKeyPair(secret.Data.Data);
            }
            catch (VaultApiException exception) when (exception.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        private async Task<IReadOnlyCollection<KeyPair>> GetKeyPairsAsync(string path, CancellationToken cancellation = default)
        {
            try
            {
                var paths = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretPathsAsync(path, mountPoint: _keyStorageOptions.MountPoint);

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
            catch (VaultApiException exception) when (exception.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return Array.Empty<KeyPair>();
            }
        }

        private async Task SetKeyPairAsync(IDictionary<string, object> data, string path, CancellationToken cancellation = default)
        {
            await _vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(
                    path: path,
                    data: data,
                    mountPoint: _keyStorageOptions.MountPoint);
        }

        private IDictionary<string, object> KeyPairToDictionary(KeyPair keyPair) => new Dictionary<string, object>
        {
            [Properties.Kid] = keyPair.Kid.ToString(),
            [Properties.Algorithm] = keyPair.Algorithm,
            [Properties.PrivateKey] = Convert.ToBase64String(keyPair.PrivateKey),
            [Properties.PublicKey] = Convert.ToBase64String(keyPair.PublicKey),
            [Properties.CreatedAt] = keyPair.CreatedAt,
            [Properties.ExpiresAt] = keyPair.ExpiresAt
        };

        private KeyPair DictionaryToKeyPair(IDictionary<string, object> dictionary) =>
            new KeyPair(Guid.Parse(dictionary[Properties.Kid].ToString()!),
                        dictionary[Properties.Algorithm].ToString()!,
                        Convert.FromBase64String(dictionary[Properties.PrivateKey].ToString()!),
                        Convert.FromBase64String(dictionary[Properties.PublicKey].ToString()!),
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
