using Auth.Domain.Events;
using Auth.Messaging.Abstractions.Providers;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
