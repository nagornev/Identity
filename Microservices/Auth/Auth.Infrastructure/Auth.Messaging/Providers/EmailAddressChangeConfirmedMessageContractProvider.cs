using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class EmailAddressChangeConfirmedMessageContractProvider : MessageContractProvider<EmailAddressChangeConfirmedDomainEvent>
    {
        public override Task<IMessageContract> Create(EmailAddressChangeConfirmedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new EmailAddressChangeConfirmedMessageContract(domainEvent.AggregateId, domainEvent.NewEmailAddress));
        }
    }
}
