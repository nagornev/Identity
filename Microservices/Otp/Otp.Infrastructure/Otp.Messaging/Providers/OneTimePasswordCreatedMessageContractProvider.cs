using MessageContracts;
using Otp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordCreatedMessageContractProvider : MessageContractProvider<OneTimePasswordCreatedDomainEvent>
    {
        public override Task<IMessageContract> Create(OneTimePasswordCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new OneTimePasswordCreatedMessageContract(domainEvent.AggregateId));
        }
    }
}
