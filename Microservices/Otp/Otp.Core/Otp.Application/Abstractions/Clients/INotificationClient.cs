using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Clients
{
    public interface INotificationClient
    {
        Task OneTimePasswordNotificationAsync(Guid subject, string oneTimePasswordValue, CancellationToken cancellation = default);
    }
}
