using DDD.Events;

namespace Otp.Domain.Events
{
    public class OneTimePasswordCreatedDomainEvent : DomainEvent
    {
        public OneTimePasswordCreatedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
