using Auth.Domain.Aggregates;
using Auth.Persistence.Contexts;
using Auth.Persistence.Extensions;
using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> Stream(ISpecification<Role> specification)
        {
            return _context.Roles.WithIncludes()
                                 .Where(specification.ToExpression())
                                 .AsEnumerable();
        }

        public IAsyncEnumerable<Role> AsyncStream(ISpecification<Role> specification)
        {
            return _context.Roles.WithIncludes()
                                 .Where(specification.ToExpression())
                                 .AsAsyncEnumerable();
        }

        public async Task<IReadOnlyCollection<Role>> FindAsync(ISpecification<Role> specification, CancellationToken cancellation = default)
        {
            return await _context.Roles.WithIncludes()
                                       .Where(specification.ToExpression())
                                       .ToArrayAsync();
        }

        public async Task<Role?> GetAsync(ISpecification<Role> specification, CancellationToken cancellation)
        {
            return await _context.Roles.WithIncludes()
                                       .FirstOrDefaultAsync(specification.ToExpression(), cancellation);
        }

        public async Task AddAsync(Role aggregate, CancellationToken cancellation = default)
        {
            _ = await _context.Roles.AddAsync(aggregate, cancellation);
        }

        public Task DeleteAsync(Role aggregate, CancellationToken cancellation = default)
        {
            _ = _context.Roles.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<Role> specification, CancellationToken cancellation = default)
        {
            return await _context.Roles.AnyAsync(specification.ToExpression(), cancellation);
        }
    }
}
