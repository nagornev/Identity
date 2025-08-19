using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class KeyRotationService<TKeyOptionsType, TKeyStorageType, TKeyFactoryType> : IKeyRotationService
        where TKeyOptionsType : KeyOptions
        where TKeyStorageType : IKeyStorage
        where TKeyFactoryType : IKeyPairFactory
    {
        private readonly TKeyStorageType _keyStorage;

        private readonly TKeyFactoryType _keyFactory;

        private readonly ITimeProvider _timeProvider;

        private readonly TKeyOptionsType _keyOptions;

        public KeyRotationService(TKeyStorageType keysStorage,
                                  TKeyFactoryType keysFactory,
                                  ITimeProvider timeProvider,
                                  IOptions<TKeyOptionsType> keyOptions)
        {
            _keyStorage = keysStorage;
            _keyFactory = keysFactory;
            _timeProvider = timeProvider;
            _keyOptions = keyOptions.Value;
        }

        public async Task<TimeSpan> RotateAsync(CancellationToken cancellation = default)
        {
            KeyPair primaryKeyPair = await _keyStorage.GetPrimaryAsync(cancellation);
            TimeSpan delay = GetRemainingRotationTimeAsync(primaryKeyPair,
                                                           TimeSpan.FromSeconds(_keyOptions.RotationInterval),
                                                           TimeSpan.FromSeconds(_timeProvider.NowUnix()));

            if (delay > TimeSpan.Zero)
                return delay;

            KeyPair newPrimaryKeyPair = _keyFactory.Create();
            await _keyStorage.SetPrimaryAsync(newPrimaryKeyPair, cancellation);

            return GetRemainingRotationTimeAsync(newPrimaryKeyPair,
                                                 TimeSpan.FromSeconds(_keyOptions.RotationInterval),
                                                 TimeSpan.FromSeconds(_timeProvider.NowUnix()));
        }

        private TimeSpan GetRemainingRotationTimeAsync(KeyPair primaryKeyPair, TimeSpan interval, TimeSpan now, CancellationToken cancellation = default)
        {
            TimeSpan createdAt = TimeSpan.FromSeconds(primaryKeyPair.CreatedAt);
            return -(now - (createdAt + interval));
        }
    }
}
