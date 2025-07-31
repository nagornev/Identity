using Auth.Persistence.Entities;

namespace Auth.Persistence.Abstractions.Repositories
{
    public interface IOutboxRepository
    {
        Task<OutboxMessage?> LockNextOutboxMessageAsync(long timestamp, CancellationToken cancellation = default);
    }
}
