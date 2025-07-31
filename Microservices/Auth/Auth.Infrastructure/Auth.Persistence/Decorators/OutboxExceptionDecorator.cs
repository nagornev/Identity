using Auth.Application.Exceptions.Infrastructures.Persistences;
using Auth.Persistence.Abstractions.Repositories;
using Auth.Persistence.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Decorators
{
    public class OutboxExceptionDecorator : IOutboxRepository
    {
        private static readonly int[] _persistenceIsUnavailableErrorTypes = [-1, 53, 10054, 10060];

        private readonly IOutboxRepository _outboxRepository;

        public OutboxExceptionDecorator(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        public async Task<OutboxMessage?> LockNextOutboxMessageAsync(long timestamp, CancellationToken cancellation = default)
        {
            try
            {
                return await _outboxRepository.LockNextOutboxMessageAsync(timestamp, cancellation);
            }
            catch (DbUpdateException exception)
                when (exception.InnerException is SqlException sqlException &&
                      _persistenceIsUnavailableErrorTypes.Contains(sqlException.Number))

            {
                throw new PersistenceUnavailableInfrastructureException();
            }
        }
    }
}
