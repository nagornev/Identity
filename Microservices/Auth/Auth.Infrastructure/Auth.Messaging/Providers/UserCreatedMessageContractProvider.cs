using Auth.Domain.Events;
using Auth.Messaging.Abstractions.Providers;
using DDD.Events;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messaging.Providers
{
    public class UserCreatedMessageContractProvider : MessageContractProvider<UserCreatedDomainEvent>
    {
        public override Task<IMessageContract> Create(UserCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new UserCreatedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
