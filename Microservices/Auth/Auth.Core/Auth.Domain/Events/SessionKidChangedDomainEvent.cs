using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionKidChangedDomainEvent : DomainEvent
    {
        public SessionKidChangedDomainEvent(Guid aggregateId,
                                            Guid kid)
            : base(aggregateId)
        {
            Kid = kid;
        }

        public Guid Kid { get; }
    }
}
