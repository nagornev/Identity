using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Application.Extensions;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Backgrounds.Processors
{
    public class KeysRotationBackgroundProcessor<TKeyOptionsType, TKeysStorageType, TKeysFactoryType>
        where TKeyOptionsType : KeyOptions
        where TKeysStorageType : IKeyStorage
        where TKeysFactoryType : IKeyPairFactory
    {
        private readonly TKeysStorageType _keysStorage;

        private readonly TKeysFactoryType _keysFactory;

        private readonly TKeyOptionsType _keyOptions;

        private readonly ITimeProvider _timeProvider;

        public KeysRotationBackgroundProcessor(TKeysStorageType keysStorage,
                                               TKeysFactoryType keysFactory,
                                               IOptions<TKeyOptionsType> keyOptions,
                                               ITimeProvider timeProvider)
        {
            _keysStorage = keysStorage;
            _keysFactory = keysFactory;
            _keyOptions = keyOptions.Value;
            _timeProvider = timeProvider;
        }

        public async Task HandleAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                TimeSpan delay = await _keysStorage.GetRemainingRotationTimeAsync(TimeSpan.FromSeconds(_keyOptions.RotationInterval),
                                                                                  TimeSpan.FromSeconds(_timeProvider.NowUnix()),
                                                                                  cancellation);
                await Task.Delay(delay, cancellation);

                IReadOnlyCollection<KeyPair> expiredKeyPairs = await GetExpiredKeyPairs(cancellation);
                foreach (KeyPair expiredKeyPair in expiredKeyPairs)
                    await _keysStorage.DeleteKeyPairAsync(expiredKeyPair.Kid, cancellation);

                KeyPair key = _keysFactory.Create();
                await _keysStorage.SetPrimaryAsync(key, cancellation);
            }
        }

        public async Task<IReadOnlyCollection<KeyPair>> GetExpiredKeyPairs(CancellationToken cancellation)
        {
            return (await _keysStorage.GetKeyPairsAsync(cancellation))
                                      .Where(x => x.ExpiresAt < _timeProvider.NowUnix())
                                      .ToArray();
        }
    }
}
