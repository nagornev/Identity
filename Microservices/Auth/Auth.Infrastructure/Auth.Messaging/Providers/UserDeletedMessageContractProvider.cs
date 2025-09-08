using Auth.Domain.Events;
using MessageContracts;

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
