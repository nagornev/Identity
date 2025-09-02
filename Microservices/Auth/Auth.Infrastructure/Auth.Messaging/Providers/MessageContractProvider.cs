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
    public abstract class MessageContractProvider<TDomainEventType> : IMessageContractProvider
        where TDomainEventType : class, IDomainEvent
    {
        public Type GetHandableType()
        {
            return typeof(TDomainEventType);
        }

        public Task<IMessageContract> Create(IDomainEvent domainEvent)
        {
            if (domainEvent is not TDomainEventType typedDomainEvent)
                throw new InvalidCastException();

            return Create(typedDomainEvent);
        }

        public abstract Task<IMessageContract> Create(TDomainEventType domainEvent);
    }
}
