using DDD.Events;
using Otp.Messaging.Abstractions.Providers;

namespace Otp.Messaging.Providers
{
    public abstract class MessageContractProvider<TDomainEventType> : IMessageContractProvider
       where TDomainEventType : class, IDomainEvent
    {
        public Type GetHandableType()
        {
            return typeof(TDomainEventType);
        }

        public Task<dynamic> Create(IDomainEvent domainEvent)
        {
            if (domainEvent is not TDomainEventType typedDomainEvent)
                throw new InvalidCastException();

            return Create(typedDomainEvent);
        }

        public abstract Task<dynamic> Create(TDomainEventType domainEvent);
    }
}
