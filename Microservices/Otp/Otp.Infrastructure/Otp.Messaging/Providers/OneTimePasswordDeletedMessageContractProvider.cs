using MessageContracts;
using Otp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordDeletedMessageContractProvider : MessageContractProvider<OneTimePasswordDeletedDomainEvent>
    {
        public override Task<IMessageContract> Create(OneTimePasswordDeletedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new OneTimePasswordDeletedMessageContract(domainEvent.AggregateId, domainEvent.Subject, domainEvent.Tag, domainEvent.CreatedAt, domainEvent.Used));
        }
    }
}
