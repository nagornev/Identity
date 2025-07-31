using Auth.Application.Abstractions.Events;
using DDD.Events;
using Microsoft.Extensions.DependencyInjection;

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
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            var handlers = (IEnumerable<object>)_serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var handleMethod = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync));
                if (handleMethod != null)
                {
                    var task = (Task)handleMethod.Invoke(handler, [domainEvent, cancellation])!;
                    await task;
                }
            }
        }
    }
}
