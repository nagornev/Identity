using Auth.Persistence.Entities;

namespace Auth.Persistence.Abstractions.Repositories
{
    public interface IOutboxRepository
    {
        Task<IReadOnlyCollection<OutboxMessage>> LockNextOutboxBatchAsync(long timestamp, CancellationToken cancellation = default);
    }
}
