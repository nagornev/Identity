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
    public class UserActivatedMessageContractProvider : MessageContractProvider<UserActivatedDomainEvent>
    {
        public override Task<IMessageContract> Create(UserActivatedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new UserActivatedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
