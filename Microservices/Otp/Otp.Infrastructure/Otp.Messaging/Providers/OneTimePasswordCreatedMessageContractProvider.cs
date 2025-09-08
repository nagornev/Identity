using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordCreatedMessageContractProvider : MessageContractProvider<OneTimePasswordCreatedDomainEvent>
    {
        public override Task<IMessageContract> Create(OneTimePasswordCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new OneTimePasswordCreatedMessageContract(domainEvent.AggregateId));
        }
    }
}
