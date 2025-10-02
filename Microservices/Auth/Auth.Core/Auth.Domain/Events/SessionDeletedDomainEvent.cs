using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionDeletedDomainEvent : DomainEvent
    {
        public SessionDeletedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
