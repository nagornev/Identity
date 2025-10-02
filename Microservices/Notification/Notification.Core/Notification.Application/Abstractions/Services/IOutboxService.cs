namespace Notification.Application.Abstractions.Services
{
    public interface IOutboxService
    {
        Task HandleAsync(CancellationToken cancellation = default);
    }
}
