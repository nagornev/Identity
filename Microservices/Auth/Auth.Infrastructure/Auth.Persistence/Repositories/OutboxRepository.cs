using Auth.Persistence.Abstractions.Repositories;
using Auth.Persistence.Contexts;
using Auth.Persistence.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private const string _lockOutboxBatchQuery =
        """
            WITH NextAggregate AS (
            SELECT TOP 1 om.StreamId, om.CreatedAt
            FROM Outbox om
            WHERE om.Processed = 0
              AND (om.LockedUntil IS NULL OR om.LockedUntil < @now)
              AND NOT EXISTS (
                  SELECT 1
                  FROM Outbox o2
                  WHERE o2.StreamId = om.StreamId
                    AND o2.LockedUntil IS NOT NULL
                    AND o2.LockedUntil > @now
              )
              AND NOT EXISTS (
                  SELECT 1
                  FROM Outbox o3
                  WHERE o3.StreamId = om.StreamId
                    AND o3.Processed = 0
                    AND (o3.LockedUntil IS NULL OR o3.LockedUntil < @now)
                    AND o3.CreatedAt < om.CreatedAt
              )
            ORDER BY om.CreatedAt
        )
        UPDATE om
        SET
            LockedAt = @now,
            LockedUntil = @now + om.Locktime
        OUTPUT inserted.*
        FROM Outbox om
        JOIN NextAggregate na ON om.StreamId = na.StreamId
        WHERE om.Processed = 0
          AND (om.LockedUntil IS NULL OR om.LockedUntil < @now);
        """;


        private readonly ApplicationDbContext _context;

        public OutboxRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<OutboxMessage>> LockNextOutboxBatchAsync(long timestamp,
                                                                                       CancellationToken cancellationToken = default)
        {
            return await _context.Outbox
                                 .FromSqlRaw(_lockOutboxBatchQuery, new SqlParameter("@now", timestamp))
                                 .ToArrayAsync(cancellationToken);
        }
    }
}
