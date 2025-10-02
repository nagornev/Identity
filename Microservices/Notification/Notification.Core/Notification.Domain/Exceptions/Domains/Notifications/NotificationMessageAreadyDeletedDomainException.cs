namespace Notification.Domain.Exceptions.Domains.Notifications
{
    public class NotificationMessageAreadyDeletedDomainException : AlreadyDomainException
    {
        private const string _message = "The notification message is aready delete.";

        public NotificationMessageAreadyDeletedDomainException()
            : base(_message)
        {
        }
    }
}
