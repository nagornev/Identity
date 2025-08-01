using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionCreatedDomainEvent : DomainEvent
    {
        public SessionCreatedDomainEvent(Guid aggregateId,
                                         Guid kid)
            : base(aggregateId)
        {
            Kid = kid;
        }

        public Guid Kid { get; }
    }
}
