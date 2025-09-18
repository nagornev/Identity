using DDD.Events;
using Otp.Messaging.Abstractions.Providers;

namespace Otp.Messaging.Providers
{
    public class MessageContractsProvider : IMessageContractsProvider
    {
        private readonly IReadOnlyDictionary<Type, IMessageContractProvider> _messageContractProviders;

        public MessageContractsProvider(IEnumerable<IMessageContractProvider> messageContractProviders)
        {
            _messageContractProviders = messageContractProviders.ToDictionary(x => x.GetHandableType(), x => x);
        }

        public async Task<dynamic> CreateAsync(IDomainEvent domainEvent, CancellationToken cancellation = default)
        {
            return _messageContractProviders.TryGetValue(domainEvent.GetType(), out var messageContractProvider) ?
                   await messageContractProvider.Create(domainEvent) :
                   throw new NotSupportedException("Unsupported domain event.");
        }
    }
}
