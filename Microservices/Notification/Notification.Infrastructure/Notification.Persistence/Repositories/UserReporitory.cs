using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;
using Notification.Domain.Aggregates;
using Notification.Persistence.Contexts;

namespace Notification.Persistence.Repositories
{
    public class UserReporitory : IRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserReporitory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAsyncEnumerable<User> AsyncStream(ISpecification<User> specification)
        {
            return _context.Users.Where(specification.ToExpression())
                                 .AsAsyncEnumerable();
        }

        public IEnumerable<User> Stream(ISpecification<User> specification)
        {
            return _context.Users.Where(specification.ToExpression())
                                 .AsEnumerable();
        }

        public async Task<IReadOnlyCollection<User>> FindAsync(ISpecification<User> specification, CancellationToken cancellation = default)
        {
            return await _context.Users.Where(specification.ToExpression())
                                       .ToArrayAsync(cancellation);
        }

        public async Task<User?> GetAsync(ISpecification<User> specification, CancellationToken cancellation = default)
        {
            return await _context.Users.FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<bool> ExistsAsync(ISpecification<User> specification, CancellationToken cancellation = default)
        {
            return await _context.Users.AnyAsync(specification.ToExpression(), cancellation);
        }

        public async Task AddAsync(User aggregate, CancellationToken cancellation = default)
        {
            await _context.Users.AddAsync(aggregate, cancellation);
        }

        public Task DeleteAsync(User aggregate, CancellationToken cancellation = default)
        {
            _context.Users.Remove(aggregate);

            return Task.CompletedTask;
        }
    }
}
