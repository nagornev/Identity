using DDD.Events;

namespace Auth.Domain.Events
{
    public class EmailAddressChangedDomainEvent : DomainEvent
    {
        public EmailAddressChangedDomainEvent(Guid aggregateId,
                                              string emailAddress)
            : base(aggregateId)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; }
    }
}
