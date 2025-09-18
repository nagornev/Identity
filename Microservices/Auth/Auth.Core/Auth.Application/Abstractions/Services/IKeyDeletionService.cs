namespace Auth.Application.Abstractions.Services
{
    public interface IKeyDeletionService
    {
        Task DeleteAsync(CancellationToken cancellation = default);
    }
}
