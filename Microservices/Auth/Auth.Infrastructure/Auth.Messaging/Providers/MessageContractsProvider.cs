using Auth.Messaging.Abstractions.Providers;
using DDD.Events;

namespace Auth.Messaging.Providers
{
    public class MessageContractsProvider : IMessageContractsProvider
    {
        private IReadOnlyDictionary<Type, IMessageContractProvider> _messageContractProviders;

        public MessageContractsProvider(IEnumerable<IMessageContractProvider> messageContractProviders)
        {
            _messageContractProviders = messageContractProviders.ToDictionary(x => x.GetHandableType(), x => x);
        }

        public async Task<dynamic> CreateAsync(IDomainEvent domainEvent)
        {
            return _messageContractProviders.TryGetValue(domainEvent.GetType(), out var messageContractProvider) ?
                   await messageContractProvider.Create(domainEvent) :
                   throw new NotSupportedException("Unsupported domain event.");
        }
    }
}
