using DDD.Events;
using MessageContracts;

namespace Auth.Messaging.Abstractions.Providers
{
    public interface IMessageContractsProvider
    {
        Task<IMessageContract> Create(IDomainEvent domainEvent);
    }
}
