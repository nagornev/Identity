using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordDeletedMessageContractProvider : MessageContractProvider<OneTimePasswordDeletedDomainEvent>
    {
        public override Task<IMessageContract> Create(OneTimePasswordDeletedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new OneTimePasswordDeletedMessageContract(domainEvent.AggregateId, domainEvent.Subject, domainEvent.Tag, domainEvent.CreatedAt, domainEvent.Used));
        }
    }
}
