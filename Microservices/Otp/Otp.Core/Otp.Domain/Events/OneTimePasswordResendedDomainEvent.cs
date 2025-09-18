using DDD.Events;

namespace Otp.Domain.Events
{
    public class OneTimePasswordResendedDomainEvent : DomainEvent
    {
        public OneTimePasswordResendedDomainEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
