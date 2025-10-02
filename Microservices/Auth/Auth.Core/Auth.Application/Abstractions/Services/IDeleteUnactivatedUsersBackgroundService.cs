namespace Auth.Application.Abstractions.Services
{
    public interface IDeleteUnactivatedUsersBackgroundService
    {
        Task DeleteUnactivatedUsersAsync(CancellationToken cancellation = default);
    }
}
