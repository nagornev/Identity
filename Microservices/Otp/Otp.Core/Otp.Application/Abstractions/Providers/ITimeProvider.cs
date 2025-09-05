using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Providers
{
    public interface ITimeProvider
    {
        long NowUnix();

        DateTime NowDateTime();
    }
}
