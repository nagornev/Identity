using DDD.Events;

namespace Auth.Domain.Events
{
    public class EmailAddressChangeConfirmedDomainEvent : DomainEvent
    {
        public EmailAddressChangeConfirmedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
