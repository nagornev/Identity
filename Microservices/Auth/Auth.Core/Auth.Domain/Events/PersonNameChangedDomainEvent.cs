using DDD.Events;

namespace Auth.Domain.Events
{
    public class PersonNameChangedDomainEvent : DomainEvent
    {
        public PersonNameChangedDomainEvent(Guid aggregateId,
                                            string personName)
            : base(aggregateId)
        {
            PersonName = personName;
        }

        public string PersonName { get; }
    }
}
