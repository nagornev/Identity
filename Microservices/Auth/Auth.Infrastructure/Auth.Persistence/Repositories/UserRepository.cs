using Auth.Domain.Aggregates;
using Auth.Persistence.Contexts;
using Auth.Persistence.Extensions;
using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns users stream by <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IEnumerable<User> Stream(ISpecification<User> specification)
        {
            return _context.Users.WithIncludes()
                                 .Where(specification.ToExpression())
                                 .AsEnumerable();
        }

        /// <summary>
        /// Returns users async stream by <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IAsyncEnumerable<User> AsyncStream(ISpecification<User> specification)
        {
            return _context.Users.WithIncludes()
                                 .Where(specification.ToExpression())
                                 .AsAsyncEnumerable();
        }

        /// <summary>
        /// Returns users collection by <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<User>> FindAsync(ISpecification<User> specification, CancellationToken cancellation = default)
        {
            return await _context.Users.WithIncludes()
                                       .Where(specification.ToExpression())
                                       .ToArrayAsync();
        }

        /// <summary>
        /// Returns user by <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<User?> GetAsync(ISpecification<User> specification, CancellationToken cancellation)
        {
            return await _context.Users.WithIncludes()
                                       .FirstOrDefaultAsync(specification.ToExpression(), cancellation);
        }

        /// <summary>
        /// Adds a new <paramref name="user"/> to the context, but does not yet exist in the database.
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task AddAsync(User aggregate, CancellationToken cancellation = default)
        {
            _ = await _context.Users.AddAsync(aggregate);
        }

        /// <summary>
        /// Marks user for deletion from the database.
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task DeleteAsync(User aggregate, CancellationToken cancellation = default)
        {
            _context.Users.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<User> specification, CancellationToken cancellation = default)
        {
            return await _context.Users.WithIncludes()
                                       .AnyAsync(specification.ToExpression(), cancellation);
        }
    }
}
