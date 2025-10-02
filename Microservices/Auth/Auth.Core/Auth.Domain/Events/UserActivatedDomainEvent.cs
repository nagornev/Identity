using DDD.Events;

namespace Auth.Domain.Events
{
    public class UserActivatedDomainEvent : DomainEvent
    {
        public UserActivatedDomainEvent(Guid aggregateId,
                                        string emailAddress)
            : base(aggregateId)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; }
    }
}
