using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class UserActivatedMessageContractProvider : MessageContractProvider<UserActivatedDomainEvent>
    {
        public override Task<dynamic> Create(UserActivatedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new UserActivatedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
