using DDD.Events;

namespace Auth.Messaging.Abstractions.Providers
{
    public interface IMessageContractsProvider
    {
        Task<dynamic> CreateAsync(IDomainEvent domainEvent);
    }
}
