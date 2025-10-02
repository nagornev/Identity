using Microsoft.EntityFrameworkCore;
using Npgsql;
using Otp.Persistence.Abstractions.Repositories;
using Otp.Persistence.Contexts;
using Otp.Persistence.Entities;

namespace Otp.Persistence.Repositories
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
