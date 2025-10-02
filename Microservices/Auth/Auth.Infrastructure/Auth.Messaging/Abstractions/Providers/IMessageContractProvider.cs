using DDD.Events;

namespace Auth.Messaging.Abstractions.Providers
{
    public interface IMessageContractProvider
    {
        Type GetHandableType();

        Task<dynamic> Create(IDomainEvent domainEvent);
    }
}
