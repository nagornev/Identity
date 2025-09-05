using DDD.Events;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Abstractions.Providers
{
    public interface IMessageContractsProvider
    {
        Task<IMessageContract> CreateAsync(IDomainEvent domainEvent, CancellationToken cancellation = default);
    }
}
