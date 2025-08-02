using Auth.Application.Abstractions.Storages;
using Auth.Application.DTOs;
using Auth.Application.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultSharp;

namespace Auth.Keys.Storages
{
    public class KeyStorage<TKeyStorageOptionsType> : IKeyStorage
        where TKeyStorageOptionsType : KeyStorageOptions
    {
        private readonly IVaultClient _vaultClient;

        private readonly TKeyStorageOptionsType _keyStorageOptions;

        public KeyStorage(IVaultClient vaultClient,
                          IOptions<TKeyStorageOptionsType> keyStorageOptions)
        {
            _vaultClient = vaultClient;
            _keyStorageOptions = keyStorageOptions.Value;
        }

        public Task AddKeyPairAsync(KeyPair keyPair, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteKeyPairAsync(Guid kid, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<KeyPair> GetKeyPairAsync(Guid kid, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<KeyPair>> GetKeyPairsAsync(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<KeyPair> GetPrimaryAsync(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
