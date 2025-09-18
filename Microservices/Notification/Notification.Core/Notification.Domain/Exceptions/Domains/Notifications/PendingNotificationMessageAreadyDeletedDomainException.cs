namespace Notification.Domain.Exceptions.Domains.Notifications
{
    public class PendingNotificationMessageAreadyDeletedDomainException : AlreadyDomainException
    {
        private const string _message = "The pending notification message is aready delete.";

        public PendingNotificationMessageAreadyDeletedDomainException()
            : base(_message)
        {
        }
    }
}
