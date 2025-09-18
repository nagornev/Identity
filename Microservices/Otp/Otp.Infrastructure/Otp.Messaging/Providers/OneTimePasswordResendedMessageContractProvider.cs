using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordResendedMessageContractProvider : MessageContractProvider<OneTimePasswordResendedDomainEvent>
    {
        public override Task<dynamic> Create(OneTimePasswordResendedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new OneTimePasswordResendedMessageContract(domainEvent.AggregateId));
        }
    }
}
