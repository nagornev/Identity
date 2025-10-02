using Notification.Persistence.Entities;

namespace Notification.Persistence.Abstractions.Repositories
{
    public interface IOutboxRepository
    {
        Task<IReadOnlyCollection<OutboxMessage>> LockNextOutboxBatchAsync(long timestamp, CancellationToken cancellation = default);
    }
}
