using MessageContracts;
using Otp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordUsedMessageContractProvider : MessageContractProvider<OneTimePasswordUsedDomainEvent>
    {
        public override Task<IMessageContract> Create(OneTimePasswordUsedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new OneTimePasswordUsedMessageContract(domainEvent.AggregateId));
        }
    }
}
