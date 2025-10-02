using DDD.Events;

namespace Auth.Domain.Events
{
    public class UserDeactivatedDomainEvent : DomainEvent
    {
        public UserDeactivatedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
