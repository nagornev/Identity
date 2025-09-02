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
    public class SessionClosedMessageContractProvider : MessageContractProvider<SessionClosedDomainEvent>
    {
        public override Task<IMessageContract> Create(SessionClosedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new SessionClosedMessageContract(domainEvent.AggregateId));
        }
    }
}
