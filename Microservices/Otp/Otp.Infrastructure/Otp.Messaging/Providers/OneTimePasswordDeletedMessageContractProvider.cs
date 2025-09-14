using MessageContracts;
using Otp.Domain.Events;

namespace Otp.Messaging.Providers
{
    public class OneTimePasswordDeletedMessageContractProvider : MessageContractProvider<OneTimePasswordDeletedDomainEvent>
    {
        public override Task<dynamic> Create(OneTimePasswordDeletedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new OneTimePasswordDeletedMessageContract(domainEvent.AggregateId, domainEvent.UserId, domainEvent.Tag, domainEvent.CreatedAt, domainEvent.Used));
        }
    }
}
