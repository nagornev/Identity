using MessageContracts;
using Otp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordResendedMessageContractProvider : MessageContractProvider<OneTimePasswordResendedDomainEvent>
    {
        public override Task<dynamic> Create(OneTimePasswordResendedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new OneTimePasswordResendedMessageContract(domainEvent.AggregateId));
        }
    }
}
