using Auth.Domain.Aggregates;
using Auth.Persistence.Contexts;
using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories
{
    public class SessionRepository : IRepository<Session>
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Session> Stream(ISpecification<Session> specification)
        {
            return _context.Sessions.Where(specification.ToExpression())
                                    .AsEnumerable();
        }

        public IAsyncEnumerable<Session> AsyncStream(ISpecification<Session> specification)
        {
            return _context.Sessions.Where(specification.ToExpression())
                                    .AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<Session>> FindAsync(ISpecification<Session> specification, CancellationToken cancellation = default)
        {
            return await _context.Sessions.Where(specification.ToExpression())
                                          .ToArrayAsync(cancellation);
        }

        public async Task<Session?> GetAsync(ISpecification<Session> specification, CancellationToken cancellation)
        {
            return await _context.Sessions.FirstOrDefaultAsync(specification.ToExpression(), cancellation);
        }

        public async Task AddAsync(Session aggregate, CancellationToken cancellation = default)
        {
            _ = await _context.Sessions.AddAsync(aggregate, cancellation);
        }

        public Task DeleteAsync(Session aggregate, CancellationToken cancellation = default)
        {
            _ = _context.Sessions.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<Session> specification, CancellationToken cancellation = default)
        {
            return await _context.Sessions.AnyAsync(specification.ToExpression(), cancellation);
        }
    }
}
