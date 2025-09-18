using DDD.Primitives;
using Notification.Domain.Events;
using Notification.Domain.Exceptions.Domains.Notifications;
using Notification.Domain.ValueObjects;
using System.Drawing;

namespace Notification.Domain.Aggregates
{
    public partial class NotificationMessage : AggregateRoot
    {
        private NotificationMessage(Guid id,
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

        public static NotificationMessage Create(Guid userId,
                                                 string channelType,
                                                 string channelValue,
                                                 string type,
                                                 string text,
                                                 long createdAt,
                                                 long expiresAt)
        {
            Guid id = Guid.NewGuid();

            NotificationMessage notification = new NotificationMessage(id,
                                                         userId,

                                                         Channel.Create(channelType, channelValue) ??
                                                         throw new ChannelNullDomainException(),

                                                         type,
                                                         text,
                                                         createdAt,
                                                         expiresAt);

            notification.AddDomainEvent(new NotificationMessageCreatedDomainEvent(notification.Id));

            return notification;
        }

        internal static NotificationMessage Restore(Guid id,
                                                    Guid userId,
                                                    string channelType,
                                                    string channelValue,
                                                    string type,
                                                    string text,
                                                    long createdAt,
                                                    long expiresAt)
        {
            NotificationMessage notification = new NotificationMessage(id,
                                                                       userId,

                                                                       Channel.Create(channelType, channelValue) ??
                                                                       throw new ChannelNullDomainException(),

                                                                       type,
                                                                       text,
                                                                       createdAt,
                                                                       expiresAt);

            return notification;
        }

        public Guid UserId { get; private set; }

        public Channel Channel { get; private set; }

        public string Type { get; private set; }

        public string Text { get; private set; }

        public long CreatedAt { get; private set; }

        public long ExpiresAt { get; private set; }

        public bool Deleted { get; private set; }

        public void MarkAsDeleted()
        {
            if (Deleted)
                throw new NotificationMessageAreadyDeletedDomainException();

            Deleted = true;

            AddDomainEvent(new NotificationMessageDeletedDomainEvent(Id, UserId, Channel.Type, Channel.Value, Type, CreatedAt));
        }
    }

    #region Ef

    public partial class NotificationMessage
    {
        private NotificationMessage()
        {
            
        }
    }

    #endregion
}
