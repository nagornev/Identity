using DDD.Repositories;
using DDD.Specifications;
using Microsoft.EntityFrameworkCore;
using Otp.Domain.Aggregates;
using Otp.Persistence.Contexts;

namespace Otp.Persistence.Repositories
{
    public class OneTimePasswordRepository : IRepository<OneTimePassword>
    {
        private readonly ApplicationDbContext _context;

        public OneTimePasswordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<OneTimePassword>> FindAsync(ISpecification<OneTimePassword> specification, CancellationToken cancellation = default)
        {
            return await _context.OneTimePasswords.Where(specification.ToExpression())
                                                  .ToArrayAsync();
        }

        public async Task<OneTimePassword?> GetAsync(ISpecification<OneTimePassword> specification, CancellationToken cancellation = default)
        {
            return await _context.OneTimePasswords.FirstOrDefaultAsync(specification.ToExpression());
        }

        public IAsyncEnumerable<OneTimePassword> AsyncStream(ISpecification<OneTimePassword> specification)
        {
            return _context.OneTimePasswords.Where(specification.ToExpression())
                                            .AsAsyncEnumerable();
        }

        public IEnumerable<OneTimePassword> Stream(ISpecification<OneTimePassword> specification)
        {
            return _context.OneTimePasswords.Where(specification.ToExpression())
                                            .AsEnumerable();
        }

        public async Task AddAsync(OneTimePassword aggregate, CancellationToken cancellation = default)
        {
            await _context.OneTimePasswords.AddAsync(aggregate);
        }

        public Task DeleteAsync(OneTimePassword aggregate, CancellationToken cancellation = default)
        {
            _context.OneTimePasswords.Remove(aggregate);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(ISpecification<OneTimePassword> specification, CancellationToken cancellation = default)
        {
            return await _context.OneTimePasswords.AnyAsync(specification.ToExpression());
        }
    }
}
