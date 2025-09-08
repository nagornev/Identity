using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class EmailAddressChangedMessageContractProvider : MessageContractProvider<EmailAddressChangedDomainEvent>
    {
        public override Task<IMessageContract> Create(EmailAddressChangedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new EmailAddressChangedMessageContract(domainEvent.AggregateId, domainEvent.EmailAddress));
        }
    }
}
