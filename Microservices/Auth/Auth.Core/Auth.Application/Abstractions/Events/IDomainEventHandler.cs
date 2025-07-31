using DDD.Events;

namespace Auth.Application.Abstractions.Events
{
    public interface IDomainEventHandler<in TDomainEventType>
        where TDomainEventType : IDomainEvent
    {
        Task HandleAsync(TDomainEventType domainEvent);
    }
}
