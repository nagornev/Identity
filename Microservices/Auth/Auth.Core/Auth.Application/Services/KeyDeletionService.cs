using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;

namespace Auth.Application.Services
{
    public abstract class KeyDeletionService<TKeyStorageType> : IKeyDeletionService
        where TKeyStorageType : IKeyStorage
    {
        private readonly TKeyStorageType _keyStorage;

        private readonly ITimeProvider _timeProvider;

        public KeyDeletionService(TKeyStorageType keyStorage,
                                  ITimeProvider timeProvider)
        {
            _keyStorage = keyStorage;
            _timeProvider = timeProvider;
        }

        public async Task DeleteAsync(CancellationToken cancellation = default)
        {
            long timestamp = _timeProvider.NowUnix();

            IReadOnlyCollection<KeyPair> keyPairs = await _keyStorage.GetKeyPairsAsync(cancellation);

            foreach (KeyPair keyPair in keyPairs)
            {
                if (timestamp > keyPair.ExpiresAt)
                {
                    await DeleteKeyPairWithRetryAsync(keyPair.Kid, cancellation: cancellation);
                }
            }
        }

        private async Task DeleteKeyPairWithRetryAsync(Guid kid,
                                                       int maxAttempts = 3,
                                                       int initialDelay = 1000,
                                                       CancellationToken cancellation = default)
        {
            int delay = initialDelay;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    await _keyStorage.DeleteKeyPairAsync(kid, cancellation);
                }
                catch when (attempt < maxAttempts)
                {
                    await Task.Delay(delay, cancellation);
                    delay *= 2;
                }
            }
        }
    }
}
