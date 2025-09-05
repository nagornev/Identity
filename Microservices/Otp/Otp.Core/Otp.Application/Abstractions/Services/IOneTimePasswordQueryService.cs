using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Services
{
    public interface IOneTimePasswordQueryService
    {
        Task<OneTimePassword> GetOneTimePasswordByIdAsync(Guid id, CancellationToken cancellation = default);

        IAsyncEnumerable<OneTimePassword> GetExpiredOneTimePasswordsAsyncEnumerable(long timestamp);
    }
}
