using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Abstractions.Services
{
    public interface IPublishEventService
    {
        Task PublishAsync<T>(T domainEvent, CancellationToken cancellation = default)
            where T : class, IDomainEvent;
    }
}
