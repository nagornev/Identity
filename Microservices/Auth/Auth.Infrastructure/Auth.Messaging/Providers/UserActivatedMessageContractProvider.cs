using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class UserActivatedMessageContractProvider : MessageContractProvider<UserActivatedDomainEvent>
    {
        public override Task<IMessageContract> Create(UserActivatedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new UserActivatedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
