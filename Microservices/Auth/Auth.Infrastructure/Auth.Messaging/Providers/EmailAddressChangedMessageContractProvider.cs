using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class EmailAddressChangedMessageContractProvider : MessageContractProvider<EmailAddressChangedDomainEvent>
    {
        public override Task<dynamic> Create(EmailAddressChangedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new EmailAddressChangedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
