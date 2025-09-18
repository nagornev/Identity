using DDD.Events;

namespace Notification.Domain.Events
{
    public class PendingNotificationMessageCreatedDomainEvent : DomainEvent
    {
        public PendingNotificationMessageCreatedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
