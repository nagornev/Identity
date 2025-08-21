using DDD.Events;
using MessageContracts;

namespace Auth.Messaging.Abstractions.Providers
{
    public interface IMessageContractProvider
    {
        IMessageContract Create(IDomainEvent domainEvent);
    }
}
