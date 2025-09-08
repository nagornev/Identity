using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class PasswordHashChangedMessageContractProvider : MessageContractProvider<PasswordHashChangedDomainEvent>
    {
        public override Task<IMessageContract> Create(PasswordHashChangedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new PasswordHashChangedMessageContract(domainEvent.AggregateId));
        }
    }
}
