using DDD.Events;

namespace Notification.Application.Abstractions.Services
{
    public interface IPublishEventService
    {
        Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent;
    }
}
