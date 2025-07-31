using Auth.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Storages
{
    public interface IKeysStorageWriter
    {
        Task AddKeyPairAsync(KeyPairDto keyPair, CancellationToken cancellation = default);

        Task DeleteKeyPairAsync(Guid kid, CancellationToken cancellation = default);

        Task<bool> RotatableAsync(CancellationToken cancellation = default);
    }
}
