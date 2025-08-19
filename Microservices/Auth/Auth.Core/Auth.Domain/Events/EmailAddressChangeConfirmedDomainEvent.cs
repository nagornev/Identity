using DDD.Events;

namespace Auth.Domain.Events
{
    public class EmailAddressChangeConfirmedDomainEvent : DomainEvent
    {
        public EmailAddressChangeConfirmedDomainEvent(Guid aggregateId, string newEmailAddress)
            : base(aggregateId)
        {
            NewEmailAddress = newEmailAddress;
        }

        public string NewEmailAddress { get; }
    }
}
