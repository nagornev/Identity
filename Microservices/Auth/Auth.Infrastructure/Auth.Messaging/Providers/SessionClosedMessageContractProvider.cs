using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class SessionClosedMessageContractProvider : MessageContractProvider<SessionClosedDomainEvent>
    {
        public override Task<dynamic> Create(SessionClosedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new SessionClosedMessageContract(domainEvent.AggregateId));
        }
    }
}
