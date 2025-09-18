using DDD.Events;

namespace Notification.Domain.Events
{
    public class PendingNotificationMessageDeletedDomainEvent : DomainEvent
    {
        public PendingNotificationMessageDeletedDomainEvent(Guid aggregateId,
                                                            Guid userId,
                                                            string channelType,
                                                            string channelValue,
                                                            string type,
                                                            long createdAt)
            : base(aggregateId)
        {
            UserId = userId;
            ChannelType = channelType;
            ChannelValue = channelValue;
            Type = type;
            CreatedAt = createdAt;
        }

        public Guid UserId { get; }

        public string ChannelType { get; }

        public string ChannelValue { get; }

        public string Type { get; }

        public long CreatedAt { get; }
    }
}
