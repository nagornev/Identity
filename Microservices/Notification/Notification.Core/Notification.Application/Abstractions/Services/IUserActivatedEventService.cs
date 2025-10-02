namespace Notification.Application.Abstractions.Services
{
    public interface IUserActivatedEventService
    {
        Task HandleAsync(Guid userId, string email, CancellationToken cancellation = default);
    }
}
