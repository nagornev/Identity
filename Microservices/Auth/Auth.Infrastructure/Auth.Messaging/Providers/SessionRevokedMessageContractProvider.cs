using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class SessionRevokedMessageContractProvider : MessageContractProvider<SessionRevokedDomainEvent>
    {
        public override Task<IMessageContract> Create(SessionRevokedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new SessionRevokedMessageContract(domainEvent.AggregateId));
        }
    }
}
