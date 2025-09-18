using Notification.Application.Abstractions.Providers;

namespace Notification.Application.Providers
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime NowDateTime()
        {
            return DateTime.UtcNow;
        }

        public long NowUnix()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
