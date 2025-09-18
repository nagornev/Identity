using DDD.Events;

namespace Notification.Messaging.Abstractions.Providers
{
    public interface IMessageContractsProvider
    {
        Task<dynamic> CreateAsync(IDomainEvent domainEvent, CancellationToken cancellation = default);
    }
}
