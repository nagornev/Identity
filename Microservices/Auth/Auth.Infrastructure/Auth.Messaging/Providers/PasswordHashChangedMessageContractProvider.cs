using Auth.Domain.Events;
using Auth.Messaging.Abstractions.Providers;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messaging.Providers
{
    public class PasswordHashChangedMessageContractProvider : MessageContractProvider<PasswordHashChangedDomainEvent>
    {
        public override Task<IMessageContract> Create(PasswordHashChangedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new PasswordHashChangedMessageContract(domainEvent.AggregateId));
        }
    }
}
