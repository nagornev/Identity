using Auth.Domain.Events;
using Auth.Messaging.Abstractions.Providers;
using DDD.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class MessageContractProvider : IMessageContractProvider
    {
        public IMessageContract Create(IDomainEvent domainEvent)
        {
            return domainEvent switch
            {
                UserCreatedDomainEvent e => new UserCreatedMessageContract(e.AggregateId, e.EmailAddress),
                UserActivatedDomainEvent e => new UserActivatedMessageContract(e.AggregateId, e.EmailAddress),
                EmailAddressChangedDomainEvent e => new EmailAddressChangedMessageContract(e.AggregateId, e.EmailAddress),
                EmailAddressChangeConfirmedDomainEvent e => new EmailAddressChangeConfirmedMessageContract(e.AggregateId, e.NewEmailAddress),
                PasswordHashChangedDomainEvent e => new PasswordHashChangedMessageContract(e.AggregateId),

                _ => throw new NotSupportedException()
            };
        }
    }
}
