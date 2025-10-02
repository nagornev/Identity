namespace Auth.Application.Abstractions.Services
{
    public interface IPasswordHashChangedEventService
    {
        Task HandleAsync(Guid userId, CancellationToken cancellation = default);
    }
}
