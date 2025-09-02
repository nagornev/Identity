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
    public class EmailAddressChangeConfirmedMessageContractProvider : MessageContractProvider<EmailAddressChangeConfirmedDomainEvent>
    {
        public override Task<IMessageContract> Create(EmailAddressChangeConfirmedDomainEvent domainEvent)
        {
            return Task.FromResult<IMessageContract>(new EmailAddressChangeConfirmedMessageContract(domainEvent.AggregateId, domainEvent.NewEmailAddress));
        }
    }
}
