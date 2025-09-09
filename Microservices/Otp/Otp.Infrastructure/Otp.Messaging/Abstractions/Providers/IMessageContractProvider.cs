using DDD.Events;
using MessageContracts;

namespace Otp.Messaging.Abstractions.Providers
{
    public interface IMessageContractProvider
    {
        Type GetHandableType();

        Task<dynamic> Create(IDomainEvent domainEvent);
    }
}
