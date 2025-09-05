using DDD.Events;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Abstractions.Providers
{
    public interface IMessageContractProvider
    {
        Type GetHandableType();

        Task<IMessageContract> Create(IDomainEvent domainEvent);
    }
}
