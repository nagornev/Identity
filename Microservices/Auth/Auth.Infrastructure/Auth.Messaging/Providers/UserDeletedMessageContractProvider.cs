using Auth.Domain.Events;
using Auth.Messaging.Abstractions.Providers;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messaging.Providers
{
    public class UserDeletedMessageContractProvider : MessageContractProvider<UserDeletedDomainEvent>
    {
        public override Task<IMessageContract> Create(UserDeletedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new UserDeletedMessageContract(domainEvent.AggregateId));
        }
    }
}
