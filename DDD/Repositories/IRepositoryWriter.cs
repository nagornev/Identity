using DDD.Primitives;

namespace DDD.Repositories
{
    public interface IRepositoryWriter<TAggregateType>
        where TAggregateType : AggregateRoot
    {
        Task AddAsync(TAggregateType aggregate, CancellationToken cancellation = default);

        Task DeleteAsync(TAggregateType aggregate, CancellationToken cancellation = default);
    }
}
