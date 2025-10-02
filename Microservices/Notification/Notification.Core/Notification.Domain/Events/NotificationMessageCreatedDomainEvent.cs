using DDD.Events;

namespace Notification.Domain.Events
{
    public class NotificationMessageCreatedDomainEvent : DomainEvent
    {
        public NotificationMessageCreatedDomainEvent(Guid aggregateId)
            : base(aggregateId)
        {
        }
    }
}
