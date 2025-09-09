using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordCreatedMessageContractProvider : MessageContractProvider<OneTimePasswordCreatedDomainEvent>
    {
        public override Task<dynamic> Create(OneTimePasswordCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new OneTimePasswordCreatedMessageContract(domainEvent.AggregateId));
        }
    }
}
