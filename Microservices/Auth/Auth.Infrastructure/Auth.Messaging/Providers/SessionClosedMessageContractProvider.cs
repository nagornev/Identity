using Auth.Domain.Events;
using MessageContracts;

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
