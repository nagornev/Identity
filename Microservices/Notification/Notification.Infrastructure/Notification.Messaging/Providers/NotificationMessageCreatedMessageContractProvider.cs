using MessageContracts;
using Notification.Domain.Events;

namespace Notification.Messaging.Providers
{
    public class NotificationMessageCreatedMessageContractProvider : MessageContractProvider<NotificationMessageCreatedDomainEvent>
    {
        public override Task<dynamic> Create(NotificationMessageCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new NotificationMessageCreatedMessageContract(domainEvent.AggregateId));
        }
    }
}
