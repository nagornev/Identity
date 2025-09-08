using DDD.Primitives;
using DDD.Repositories;
using DDD.Specifications;
using Microsoft.Extensions.Logging;

namespace Otp.Persistence.Decorators
{
    public class RepositoryExceptionDecorator<TAggregateType> : PersistenceExceptionDecorator, IRepository<TAggregateType>
       where TAggregateType : AggregateRoot
    {

        private readonly IRepository<TAggregateType> _repository;

        private readonly ILogger<RepositoryExceptionDecorator<TAggregateType>> _logger;

        public RepositoryExceptionDecorator(IRepository<TAggregateType> repository,
                                            ILogger<RepositoryExceptionDecorator<TAggregateType>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IAsyncEnumerable<TAggregateType> AsyncStream(ISpecification<TAggregateType> specification)
        {
            return Call(() => _repository.AsyncStream(specification));
        }

        public IEnumerable<TAggregateType> Stream(ISpecification<TAggregateType> specification)
        {
            return Call(() => _repository.Stream(specification));
        }

        public async Task<IReadOnlyCollection<TAggregateType>> FindAsync(ISpecification<TAggregateType> specification, CancellationToken cancellation = default)
        {
            return await CallAsync(() => _repository.FindAsync(specification, cancellation));
        }

        public async Task<TAggregateType?> GetAsync(ISpecification<TAggregateType> specification, CancellationToken cancellation = default)
        {
            return await CallAsync(() => _repository.GetAsync(specification, cancellation));
        }

        public async Task<bool> ExistsAsync(ISpecification<TAggregateType> specification, CancellationToken cancellation = default)
        {
            return await CallAsync(() => _repository.ExistsAsync(specification, cancellation));
        }

        public async Task AddAsync(TAggregateType aggregate, CancellationToken cancellation = default)
        {
            await CallAsync(() => _repository.AddAsync(aggregate, cancellation));
        }

        public async Task DeleteAsync(TAggregateType aggregate, CancellationToken cancellation = default)
        {
            await CallAsync(() => _repository.DeleteAsync(aggregate, cancellation));
        }

        protected override void LogError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}
