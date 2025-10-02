using MessageContracts;
using Notification.Domain.Events;

namespace Notification.Messaging.Providers
{
    public class PendingNotificationMessageCreatedMessageContractProvider : MessageContractProvider<PendingNotificationMessageCreatedDomainEvent>
    {
        public override Task<dynamic> Create(PendingNotificationMessageCreatedDomainEvent domainEvent)
        {
            return Task.FromResult<dynamic>(new PendingNotificationMessageCreatedMessageContract(domainEvent.AggregateId));
        }
    }
}
