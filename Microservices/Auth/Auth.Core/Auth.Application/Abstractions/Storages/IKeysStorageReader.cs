using Auth.Application.DTOs;

namespace Auth.Application.Abstractions.Storages
{
    public interface IKeysStorageReader
    {
        Task<IReadOnlyCollection<KeyPairDto>> GetKeyPairsAsync(CancellationToken cancellation = default);

        Task<KeyDto> GetPrivateKeyAsync(CancellationToken cancellation = default);

        Task<IReadOnlyCollection<KeyDto>> GetPublicKeysAsync(CancellationToken cancellation = default);

        Task<int> GetRotatableDelayAsync(CancellationToken cancellation = default);
    }
}
