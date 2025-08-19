using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionRevokedDomainEvent : DomainEvent
    {
        public SessionRevokedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
