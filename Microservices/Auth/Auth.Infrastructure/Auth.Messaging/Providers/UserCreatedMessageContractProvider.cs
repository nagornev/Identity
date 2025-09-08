using Auth.Domain.Events;
using MessageContracts;

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
