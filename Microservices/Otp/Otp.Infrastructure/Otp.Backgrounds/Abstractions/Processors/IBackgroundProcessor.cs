using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Backgrounds.Abstractions.Processors
{
    public interface IBackgroundProcessor
    {
        Task StartAsync(CancellationToken cancellation = default);
    }
}
