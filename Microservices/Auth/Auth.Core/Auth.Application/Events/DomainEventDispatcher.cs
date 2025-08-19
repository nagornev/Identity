using Auth.Application.Abstractions.Events;
using DDD.Events;

namespace Auth.Application.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellation = default)
        {
            var type = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handler = (IDomainEventHandler<IDomainEvent>)_serviceProvider.GetService(type)!;

            await handler.HandleAsync(domainEvent);
        }
    }
}
