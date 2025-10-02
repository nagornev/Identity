using Otp.Persistence.Entities;

namespace Otp.Persistence.Abstractions.Repositories
{
    public interface IOutboxRepository
    {
        Task<IReadOnlyCollection<OutboxMessage>> LockNextOutboxBatchAsync(long timestamp, CancellationToken cancellation = default);
    }
}
