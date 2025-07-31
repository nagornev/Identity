namespace Auth.Application.Abstractions.Services
{
    public interface IOutboxService
    {
        Task HandleMessageAsync(CancellationToken cancellation = default);
    }
}
