using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Storages
{
    public interface IKeyStorageWriter
    {
        Task SetPrimaryAsync(KeyPair keyPair, CancellationToken cancellation = default);

        Task DeleteKeyPairAsync(Guid kid, CancellationToken cancellation = default);
    }
}
