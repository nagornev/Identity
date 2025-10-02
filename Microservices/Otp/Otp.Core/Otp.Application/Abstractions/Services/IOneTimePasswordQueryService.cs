using Otp.Domain.Aggregates;

namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordQueryService
    {
        Task<OneTimePassword> GetOneTimePasswordByIdAsync(Guid id, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<OneTimePassword>> GetExpiredOneTimePasswordsAsync(long timestamp);
    }
}
