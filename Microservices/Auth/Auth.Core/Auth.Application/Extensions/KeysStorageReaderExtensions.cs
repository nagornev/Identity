using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;

namespace Auth.Application.Extensions
{
    public static class KeysStorageReaderExtensions
    {
        public static async Task<TimeSpan> GetRemainingRotationTimeAsync(this IKeyStorageReader keysStorageReader, TimeSpan interval, TimeSpan now, CancellationToken cancellation = default)
        {
            KeyPair primaryKey = await keysStorageReader.GetPrimaryAsync(cancellation);

            TimeSpan createdAt = TimeSpan.FromSeconds(primaryKey.CreatedAt);

            return -(now - (createdAt + interval));
        }
    }
}
