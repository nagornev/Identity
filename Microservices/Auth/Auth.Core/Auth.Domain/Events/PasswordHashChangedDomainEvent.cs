using DDD.Events;

namespace Auth.Domain.Events
{
    public class PasswordHashChangedDomainEvent : DomainEvent
    {
        public PasswordHashChangedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
