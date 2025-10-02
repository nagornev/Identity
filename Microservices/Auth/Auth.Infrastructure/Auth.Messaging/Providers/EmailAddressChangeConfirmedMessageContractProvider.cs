using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class EmailAddressChangeConfirmedMessageContractProvider : MessageContractProvider<EmailAddressChangeConfirmedDomainEvent>
    {
        public override Task<dynamic> Create(EmailAddressChangeConfirmedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new EmailAddressChangeConfirmedMessageContract(domainEvent.AggregateId, domainEvent.NewEmailAddress));
        }
    }
}
