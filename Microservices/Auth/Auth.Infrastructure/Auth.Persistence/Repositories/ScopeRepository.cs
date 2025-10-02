using Auth.Domain.Aggregates;
using Auth.Persistence.Contexts;
using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories
{
    public class ScopeRepository : IRepository<Scope>
    {
        private readonly ApplicationDbContext _context;

        public ScopeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Scope> Stream(ISpecification<Scope> specification)
        {
            return _context.Scopes.Where(specification.ToExpression())
                                  .AsEnumerable();
        }

        public IAsyncEnumerable<Scope> AsyncStream(ISpecification<Scope> specification)
        {
            return _context.Scopes.Where(specification.ToExpression())
                                  .AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<Scope>> FindAsync(ISpecification<Scope> specification, CancellationToken cancellation = default)
        {
            return await _context.Scopes.Where(specification.ToExpression())
                                        .ToArrayAsync(cancellation);
        }

        public async Task<Scope?> GetAsync(ISpecification<Scope> specification, CancellationToken cancellation)
        {
            return await _context.Scopes.FirstOrDefaultAsync(specification.ToExpression(), cancellation);
        }

        public async Task AddAsync(Scope aggregate, CancellationToken cancellation = default)
        {
            _ = await _context.Scopes.AddAsync(aggregate, cancellation);
        }

        public Task DeleteAsync(Scope aggregate, CancellationToken cancellation = default)
        {
            _ = _context.Scopes.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<Scope> specification, CancellationToken cancellation = default)
        {
            return await _context.Scopes.AnyAsync(specification.ToExpression(), cancellation);
        }


    }
}
