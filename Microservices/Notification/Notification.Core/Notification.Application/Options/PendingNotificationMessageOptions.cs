namespace Notification.Application.Options
{
    public class PendingNotificationMessageOptions
    {
        public PendingNotificationMessageOptions(int lifetime, string deletionInterval)
        {
            Lifetime = lifetime;
            DeletionInterval = deletionInterval;
        }

        public int Lifetime { get; }

        public string DeletionInterval { get; }
    }
}
