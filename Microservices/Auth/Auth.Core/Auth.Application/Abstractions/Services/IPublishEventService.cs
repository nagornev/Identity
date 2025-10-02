using DDD.Events;

namespace Auth.Application.Abstractions.Services
{
    public interface IPublishEventService
    {
        Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent;
    }
}
