using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;

namespace Auth.Application.Services
{
    public class KeyRotationService<TKeyStorageType, TKeyFactoryType> : IKeyRotationService
        where TKeyStorageType : IKeyStorage
        where TKeyFactoryType : IKeyPairFactory
    {
        private readonly TKeyStorageType _keyStorage;

        private readonly TKeyFactoryType _keyFactory;

        public KeyRotationService(TKeyStorageType keysStorage,
                                  TKeyFactoryType keysFactory)
        {
            _keyStorage = keysStorage;
            _keyFactory = keysFactory;
        }

        public async Task RotateAsync(CancellationToken cancellation = default)
        {
            KeyPair newPrimaryKeyPair = _keyFactory.Create();
            await _keyStorage.SetPrimaryAsync(newPrimaryKeyPair, cancellation);
        }
    }
}
