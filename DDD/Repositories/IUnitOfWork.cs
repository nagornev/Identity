namespace DDD.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellation = default);
    }
}
