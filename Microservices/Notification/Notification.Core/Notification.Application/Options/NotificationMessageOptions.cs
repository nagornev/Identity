namespace Notification.Application.Options
{
    public class NotificationMessageOptions
    {
        public NotificationMessageOptions(int lifetime, string deletionInterval)
        {
            Lifetime = lifetime;
            DeletionInterval = deletionInterval;
        }

        public int Lifetime { get; }

        public string DeletionInterval { get; }
    }
}
