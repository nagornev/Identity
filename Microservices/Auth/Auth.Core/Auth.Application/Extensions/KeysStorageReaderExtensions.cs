using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;

namespace Auth.Application.Extensions
{
    public static class KeysStorageReaderExtensions
    {
        public static async Task<KeyDto> GetPrivateKeyAsync(this IKeyStorageReader keysStorageReader, CancellationToken cancellation = default)
        {
            KeyPairDto primaryKey = await keysStorageReader.GetPrimaryAsync(cancellation);

            return new KeyDto(primaryKey.Kid, primaryKey.Algorithm, primaryKey.PrivateKey);
        }

        public static async Task<IReadOnlyCollection<KeyDto>> GetPublicKeysAsync(this IKeyStorageReader keysStorageReader, CancellationToken cancellation = default)
        {
            IReadOnlyCollection<KeyPairDto> keys = await keysStorageReader.GetKeyPairsAsync(cancellation);

            return keys.Select(x => new KeyDto(x.Kid, x.Algorithm, x.PublicKey))
                       .ToArray();
        }

        public static async Task<TimeSpan> GetRemainingRotationTimeAsync(this IKeyStorageReader keysStorageReader, TimeSpan interval, TimeSpan now, CancellationToken cancellation = default)
        {
            KeyPairDto primaryKey = await keysStorageReader.GetPrimaryAsync(cancellation);

            TimeSpan createdAt = TimeSpan.FromSeconds(primaryKey.CreatedAt);

            return now < (createdAt + interval)?
                   createdAt + interval - now:
                   TimeSpan.Zero;
        }
    }
}
