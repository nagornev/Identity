using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Abstractions.Services
{
    public interface ILogService
    {
        Task LogError(Exception exception, CancellationToken cancellation = default);
    }
}
