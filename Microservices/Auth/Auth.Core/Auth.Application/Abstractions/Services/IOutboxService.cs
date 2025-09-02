namespace Auth.Application.Abstractions.Services
{
    public interface IOutboxService
    {
        Task HandleAsync(CancellationToken cancellation = default);
    }
}
