using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordUsedMessageContractProvider : MessageContractProvider<OneTimePasswordUsedDomainEvent>
    {
        public override Task<IMessageContract> Create(OneTimePasswordUsedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new OneTimePasswordUsedMessageContract(domainEvent.AggregateId));
        }
    }
}
