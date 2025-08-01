using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionClosedDomainEvent : DomainEvent
    {
        public SessionClosedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
