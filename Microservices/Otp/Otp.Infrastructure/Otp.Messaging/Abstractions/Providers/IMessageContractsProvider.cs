using DDD.Events;
using MessageContracts;

namespace Otp.Messaging.Abstractions.Providers
{
    public interface IMessageContractsProvider
    {
        Task<IMessageContract> CreateAsync(IDomainEvent domainEvent, CancellationToken cancellation = default);
    }
}
