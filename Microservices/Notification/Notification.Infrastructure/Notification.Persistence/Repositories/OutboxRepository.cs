using Microsoft.EntityFrameworkCore;
using Notification.Persistence.Abstractions.Repositories;
using Notification.Persistence.Contexts;
using Notification.Persistence.Entities;
using Npgsql;

namespace Notification.Persistence.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private const string _lockOutboxBatchQuery =
        """
            WITH NextAggregate AS (
            SELECT om."StreamId", om."CreatedAt"
            FROM "Outbox" om
            WHERE om."Processed" = FALSE
              AND (om."LockedUntil" IS NULL OR om."LockedUntil" < @now)
              AND NOT EXISTS (
                  SELECT 1
                  FROM "Outbox" o2
                  WHERE o2."StreamId" = om."StreamId"
                    AND o2."LockedUntil" IS NOT NULL
                    AND o2."LockedUntil" > @now
              )
              AND NOT EXISTS (
                  SELECT 1
                  FROM "Outbox" o3
                  WHERE o3."StreamId" = om."StreamId"
                    AND o3."Processed" = FALSE
                    AND (o3."LockedUntil" IS NULL OR o3."LockedUntil" < @now)
                    AND o3."CreatedAt" < om."CreatedAt"
              )
            ORDER BY om."CreatedAt"
            LIMIT 1
        )
        UPDATE "Outbox" om
        SET
            "LockedAt" = @now,
            "LockedUntil" = @now + om."Locktime"
        FROM NextAggregate na
        WHERE om."StreamId" = na."StreamId"
          AND om."Processed" = FALSE
          AND (om."LockedUntil" IS NULL OR om."LockedUntil" < @now)
        RETURNING om.*;
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
                                 .FromSqlRaw(_lockOutboxBatchQuery, new NpgsqlParameter("@now", timestamp))
                                 .ToArrayAsync(cancellationToken);
        }
    }
}
