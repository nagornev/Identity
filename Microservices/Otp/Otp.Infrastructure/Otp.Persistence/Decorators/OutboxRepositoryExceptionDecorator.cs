using Microsoft.Extensions.Logging;
using Otp.Persistence.Abstractions.Repositories;
using Otp.Persistence.Entities;

namespace Otp.Persistence.Decorators
{
    public class OutboxRepositoryExceptionDecorator : PersistenceExceptionDecorator, IOutboxRepository
    {
        private readonly IOutboxRepository _outboxRepository;

        private readonly ILogger<OutboxRepositoryExceptionDecorator> _logger;

        public OutboxRepositoryExceptionDecorator(IOutboxRepository outboxRepository,
                                        ILogger<OutboxRepositoryExceptionDecorator> logger)
        {
            _outboxRepository = outboxRepository;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<OutboxMessage>> LockNextOutboxBatchAsync(long timestamp, CancellationToken cancellation = default)
        {
            return await CallAsync(() => _outboxRepository.LockNextOutboxBatchAsync(timestamp, cancellation));
        }

        protected override void LogError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}
