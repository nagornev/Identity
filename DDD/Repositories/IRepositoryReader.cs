using DDD.Primitives;
using DDD.Specifications;

namespace DDD.Repositories
{
    public interface IRepositoryReader<TAggregateType>
        where TAggregateType : AggregateRoot
    {
        IEnumerable<TAggregateType> Stream(ISpecification<TAggregateType> specification);

        IAsyncEnumerable<TAggregateType> AsyncStream(ISpecification<TAggregateType> specification);

        Task<IReadOnlyCollection<TAggregateType>> FindAsync(ISpecification<TAggregateType> specification, CancellationToken cancellation = default);

        Task<TAggregateType?> GetAsync(ISpecification<TAggregateType> specification, CancellationToken cancellation = default);

        Task<bool> ExistsAsync(ISpecification<TAggregateType> specification, CancellationToken cancellation = default);
    }
}
