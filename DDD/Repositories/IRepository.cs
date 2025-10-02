using DDD.Primitives;

namespace DDD.Repositories
{
    public interface IRepository<TAggregateType> : IRepositoryReader<TAggregateType>, IRepositoryWriter<TAggregateType>
        where TAggregateType : AggregateRoot
    {
    }
}
