using DDD.Events;

namespace Notification.Messaging.Abstractions.Providers
{
    public interface IMessageContractProvider
    {
        Type GetHandableType();

        Task<dynamic> Create(IDomainEvent domainEvent);
    }
}
