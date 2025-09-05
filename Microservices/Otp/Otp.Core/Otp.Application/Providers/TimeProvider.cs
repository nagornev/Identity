using Otp.Application.Abstractions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
