using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class PasswordHashChangedMessageContractProvider : MessageContractProvider<PasswordHashChangedDomainEvent>
    {
        public override Task<dynamic> Create(PasswordHashChangedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new PasswordHashChangedMessageContract(domainEvent.AggregateId));
        }
    }
}
