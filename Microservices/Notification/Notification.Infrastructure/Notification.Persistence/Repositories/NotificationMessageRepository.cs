using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;
using Notification.Domain.Aggregates;
using Notification.Persistence.Contexts;

namespace Notification.Persistence.Repositories
{
    public class NotificationMessageRepository : IRepository<NotificationMessage>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NotificationMessageRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<NotificationMessage> AsyncStream(ISpecification<NotificationMessage> specification)
        {
            return _applicationDbContext.NotificationMessages.Where(specification.ToExpression())
                                                             .AsAsyncEnumerable();
        }

        public IEnumerable<NotificationMessage> Stream(ISpecification<NotificationMessage> specification)
        {
            return _applicationDbContext.NotificationMessages.Where(specification.ToExpression())
                                                             .AsEnumerable();
        }

        public async Task<IReadOnlyCollection<NotificationMessage>> FindAsync(ISpecification<NotificationMessage> specification, CancellationToken cancellation = default)
        {
            return await _applicationDbContext.NotificationMessages.Where(specification.ToExpression())
                                                                   .ToArrayAsync(cancellation);
        }

        public async Task<NotificationMessage?> GetAsync(ISpecification<NotificationMessage> specification, CancellationToken cancellation = default)
        {
            return await _applicationDbContext.NotificationMessages.FirstOrDefaultAsync(specification.ToExpression(), cancellation);
        }


        public async Task AddAsync(NotificationMessage aggregate, CancellationToken cancellation = default)
        {
            await _applicationDbContext.NotificationMessages.AddAsync(aggregate, cancellation);
        }


        public Task DeleteAsync(NotificationMessage aggregate, CancellationToken cancellation = default)
        {
            _applicationDbContext.NotificationMessages.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<NotificationMessage> specification, CancellationToken cancellation = default)
        {
            return await _applicationDbContext.NotificationMessages.AnyAsync(specification.ToExpression(), cancellation);

        }


    }
}
