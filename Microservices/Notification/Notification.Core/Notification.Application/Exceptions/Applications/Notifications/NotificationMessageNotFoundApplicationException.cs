namespace Notification.Application.Exceptions.Applications.Notifications
{
    internal class NotificationMessageNotFoundApplicationException : NotFoundApplicationException
    {
        public NotificationMessageNotFoundApplicationException(Guid id)
            : base($"The notification message with id ({id}) was not found.")
        {
        }
    }
}
