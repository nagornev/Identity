using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class UserDeletedMessageContractProvider : MessageContractProvider<UserDeletedDomainEvent>
    {
        public override Task<dynamic> Create(UserDeletedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new UserDeletedMessageContract(domainEvent.AggregateId));
        }
    }
}
