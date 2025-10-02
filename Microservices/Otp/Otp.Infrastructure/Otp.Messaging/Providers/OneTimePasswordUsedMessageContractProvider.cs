using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordUsedMessageContractProvider : MessageContractProvider<OneTimePasswordUsedDomainEvent>
    {
        public override Task<dynamic> Create(OneTimePasswordUsedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new OneTimePasswordUsedMessageContract(domainEvent.AggregateId));
        }
    }
}
