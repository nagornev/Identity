using DDD.Events;

namespace Auth.Domain.Events
{
    public sealed class UserCreatedDomainEvent : DomainEvent
    {
        public UserCreatedDomainEvent(Guid aggregateId, string emailAddress)
            : base(aggregateId)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; }
    }
}
