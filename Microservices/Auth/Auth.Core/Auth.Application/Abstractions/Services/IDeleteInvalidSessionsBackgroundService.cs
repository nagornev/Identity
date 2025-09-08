namespace Auth.Application.Abstractions.Services
{
    public interface IDeleteInvalidSessionsBackgroundService
    {
        Task DeleteInvalidSessionsAsync(CancellationToken cancellation = default);
    }
}
