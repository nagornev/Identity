namespace Notification.Application.Exceptions.Applications.Notifications
{
    public class PendingNotificationMessageNotFoundApplicationException : NotFoundApplicationException
    {
        public PendingNotificationMessageNotFoundApplicationException(Guid id)
            : base($"The pending notification message with id ({id}) was not found.")
        {
        }
    }
}
