namespace Notification.Application.Abstractions.Services
{
    public interface IEmailAddressChangedEventService
    {
        Task HandleAsync(Guid userId, string email, CancellationToken cancellation = default);
    }
}
