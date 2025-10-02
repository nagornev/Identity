namespace Auth.Application.Abstractions.Services
{
    public interface IUserCreatedEventService
    {
        Task HandleAsync(Guid userId, string emailAddress, CancellationToken cancellation = default);
    }
}
