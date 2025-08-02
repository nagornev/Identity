using Auth.Application.Abstractions.Storages;

namespace Auth.Application.Abstractions.Services
{
    public interface IKeyRotationService
    {
        Task<int> RotateAsync(CancellationToken cancellation = default);
    }
}
