using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Security.Options;
using Microsoft.Extensions.Options;

namespace Auth.Security.Storages
{
    public abstract class KeyStorage<TKeyStorageOptions> : IKeyStorage
        where TKeyStorageOptions : KeyStorageOptions
    {
        protected TKeyStorageOptions KeyStorageOptions { get; }

        public KeyStorage(IOptions<TKeyStorageOptions> keyStorageOptions)
        {
            KeyStorageOptions = keyStorageOptions.Value;
        }

        public abstract Task SetPrimaryAsync(KeyPair keyPair, CancellationToken cancellation = default);

        public abstract Task DeleteKeyPairAsync(Guid kid, CancellationToken cancellation = default);

        public abstract Task<KeyPair> GetKeyPairAsync(Guid kid, CancellationToken cancellation = default);

        public abstract Task<IReadOnlyCollection<KeyPair>> GetKeyPairsAsync(CancellationToken cancellation = default);

        public abstract Task<KeyPair> GetPrimaryAsync(CancellationToken cancellation = default);
    }
}
