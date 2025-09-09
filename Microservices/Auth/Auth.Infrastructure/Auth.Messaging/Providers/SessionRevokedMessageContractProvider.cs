using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class SessionRevokedMessageContractProvider : MessageContractProvider<SessionRevokedDomainEvent>
    {
        public override Task<dynamic> Create(SessionRevokedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new SessionRevokedMessageContract(domainEvent.AggregateId));
        }
    }
}
