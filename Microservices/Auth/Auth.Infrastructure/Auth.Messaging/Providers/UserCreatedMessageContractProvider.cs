using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class UserCreatedMessageContractProvider : MessageContractProvider<UserCreatedDomainEvent>
    {
        public override Task<dynamic> Create(UserCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new UserCreatedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
