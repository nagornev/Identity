using Otp.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Persistence.Abstractions.Repositories
{
    public interface IOutboxRepository
    {
        Task<IReadOnlyCollection<OutboxMessage>> LockNextOutboxBatchAsync(long timestamp, CancellationToken cancellation = default);
    }
}
