using Otp.Application.Abstractions.Providers;

namespace Otp.Application.Providers
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
