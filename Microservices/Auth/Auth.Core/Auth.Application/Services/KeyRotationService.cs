using Auth.Application.Abstractions.Factories.Keys;
using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Abstractions.Storages;
using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class KeyRotationService<TKeyOptionsType, TKeysStorageType, TKeysFactoryType>: IKeyRotationService
        where TKeyOptionsType : KeyOptions
        where TKeysStorageType : IKeyStorage
        where TKeysFactoryType : IKeyPairFactory
    {
        public KeyRotationService(TKeysStorageType keysStorage,
                                  TKeysFactoryType keysFactory,
                                  ITimeProvider timeProvider,
                                  IOptions<TKeyOptionsType> keyOptions)
        {
            
        }

        public Task<int> RotateAsync(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
