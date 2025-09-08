using DDD.Events;

namespace Auth.Domain.Events
{
    public class UserDeletedDomainEvent : DomainEvent
    {
        public UserDeletedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
