using DDD.Events;

namespace Otp.Domain.Events
{
    public class OneTimePasswordUsedDomainEvent : DomainEvent
    {
        public OneTimePasswordUsedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
