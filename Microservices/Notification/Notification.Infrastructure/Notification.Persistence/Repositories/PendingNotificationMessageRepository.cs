using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;
using Notification.Domain.Aggregates;
using Notification.Persistence.Contexts;

namespace Notification.Persistence.Repositories
{
    public class PendingNotificationMessageRepository : IRepository<PendingNotificationMessage>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PendingNotificationMessageRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<PendingNotificationMessage> AsyncStream(ISpecification<PendingNotificationMessage> specification)
        {
            return _applicationDbContext.PendingNotificationMessages.Where(specification.ToExpression())
                                                                    .AsAsyncEnumerable();
        }

        public IEnumerable<PendingNotificationMessage> Stream(ISpecification<PendingNotificationMessage> specification)
        {
            return _applicationDbContext.PendingNotificationMessages.Where(specification.ToExpression())
                                                                    .AsEnumerable();
        }

        public async Task<IReadOnlyCollection<PendingNotificationMessage>> FindAsync(ISpecification<PendingNotificationMessage> specification, CancellationToken cancellation = default)
        {
            return await _applicationDbContext.PendingNotificationMessages.Where(specification.ToExpression())
                                                                          .ToArrayAsync(cancellation);
        }

        public async Task<PendingNotificationMessage?> GetAsync(ISpecification<PendingNotificationMessage> specification, CancellationToken cancellation = default)
        {
            return await _applicationDbContext.PendingNotificationMessages.FirstOrDefaultAsync(specification.ToExpression(), cancellation);
        }


        public async Task AddAsync(PendingNotificationMessage aggregate, CancellationToken cancellation = default)
        {
            await _applicationDbContext.PendingNotificationMessages.AddAsync(aggregate, cancellation);
        }


        public Task DeleteAsync(PendingNotificationMessage aggregate, CancellationToken cancellation = default)
        {
            _applicationDbContext.PendingNotificationMessages.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<PendingNotificationMessage> specification, CancellationToken cancellation = default)
        {
            return await _applicationDbContext.PendingNotificationMessages.AnyAsync(specification.ToExpression(), cancellation);

        }
    }
}
