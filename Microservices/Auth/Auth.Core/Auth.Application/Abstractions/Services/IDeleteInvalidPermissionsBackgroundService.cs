namespace Auth.Application.Abstractions.Services
{
    public interface IDeleteInvalidPermissionsBackgroundService
    {
        Task DeleteInvalidPermissionsAsync(CancellationToken cancellation = default);
    }
}
