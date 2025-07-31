using Auth.Persistence.Abstractions.Repositories;
using Auth.Persistence.Contexts;
using Auth.Persistence.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private const string _lockNextOutboxMessageQuery =
        """
            WITH NextMessage AS (
                SELECT TOP 1 om.*
                FROM OutboxMessages om
                WHERE om.Processed = 0
                  AND (om.LockedUntil IS NULL OR om.LockedUntil < @now)
                  AND NOT EXISTS (
                      SELECT 1
                      FROM OutboxMessages o2
                      WHERE o2.StreamId = om.StreamId
                        AND o2.LockedUntil IS NOT NULL
                        AND o2.LockedUntil > @now
                  )
                ORDER BY om.CreatedAt
            )
            UPDATE om
            SET
                LockedAt = @now,
                LockedUntil = @now + om.Locktime
            OUTPUT inserted.*
            FROM OutboxMessages om
            JOIN NextMessage nm ON om.Id = nm.Id;
        """;


        private readonly ApplicationDbContext _context;

        public OutboxRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OutboxMessage?> LockNextOutboxMessageAsync(long timestamp,
                                                                     CancellationToken cancellationToken = default)
        {
            return await _context.Outbox
                                 .FromSqlRaw(_lockNextOutboxMessageQuery, new SqlParameter("@now", timestamp))
                                 .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
