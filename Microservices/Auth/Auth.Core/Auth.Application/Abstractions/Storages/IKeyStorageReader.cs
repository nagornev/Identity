using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Storages
{
    public interface IKeyStorageReader
    {
        Task<KeyPair> GetPrimaryAsync(CancellationToken cancellation = default);

        Task<KeyPair> GetKeyPairAsync(Guid kid, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<KeyPair>> GetKeyPairsAsync(CancellationToken cancellation = default);
    }
}
