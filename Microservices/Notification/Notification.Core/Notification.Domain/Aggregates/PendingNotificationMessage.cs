using DDD.Primitives;
using Notification.Domain.Events;
using Notification.Domain.Exceptions.Domains.Notifications;
using Notification.Domain.ValueObjects;

namespace Notification.Domain.Aggregates
{
    /// <summary>
    /// Uses for creating notifications about user registraction. After activation this notification is transferred to Notification.
    /// </summary>
    public partial class PendingNotificationMessage : AggregateRoot
    {
        private PendingNotificationMessage(Guid id,
                                           Guid userId,
                                           Channel channel,
                                           string type,
                                           string text,
                                           long createdAt,
                                           long expiresAt)
        {
            Id = id;
            UserId = userId;
            Channel = channel;
            Type = type;
            Text = text;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public static PendingNotificationMessage Create(Guid userId,
                                                 string channelType,
                                                 string channelValue,
                                                 string type,
                                                 string text,
                                                 long createdAt,
                                                 long expiresAt)
        {
            Guid id = Guid.NewGuid();

            PendingNotificationMessage pendingNotification = new PendingNotificationMessage(id,
                                                                              userId,

                                                                              Channel.Create(channelType, channelValue) ??
                                                                              throw new ChannelNullDomainException(),

                                                                              type,
                                                                              text,
                                                                              createdAt,
                                                                              expiresAt);

            pendingNotification.AddDomainEvent(new PendingNotificationMessageCreatedDomainEvent(pendingNotification.Id));

            return pendingNotification;
        }

        public Guid UserId { get; private set; }

        public Channel Channel { get; private set; }

        public string Type { get; private set; }

        public string Text { get; private set; }

        public long CreatedAt { get; private set; }

        public long ExpiresAt { get; private set; }

        public bool Deleted { get; private set; }

        public NotificationMessage ToNotificationMessage()
        {
            return NotificationMessage.Restore(Id,
                                               UserId,
                                               Channel.Type,
                                               Channel.Value,
                                               Type,
                                               Text,
                                               CreatedAt,
                                               ExpiresAt);
        }

        public void MarkAsDeleted()
        {
            if (Deleted)
                throw new PendingNotificationMessageAreadyDeletedDomainException();

            Deleted = true;

            AddDomainEvent(new PendingNotificationMessageDeletedDomainEvent(Id, UserId, Channel.Type, Channel.Value, Type, CreatedAt));
        }
    }

    #region Ef

    public partial class PendingNotificationMessage
    {
        private PendingNotificationMessage()
        {
            
        }
    }

    #endregion
}
