using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Storages
{
    public interface IKeyStorageReader
    {
        Task<KeyPairDto> GetPrimaryAsync(CancellationToken cancellation = default);

        Task<KeyPairDto> GetKeyPairAsync(Guid kid, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<KeyPairDto>> GetKeyPairsAsync(CancellationToken cancellation = default);
    }
}
