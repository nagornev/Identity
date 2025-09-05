using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Services
{
    public interface IDeleteInvalidOneTimePasswordsBackgroundService
    {
        Task DeleteAsync(CancellationToken cancellation = default);
    }
}
