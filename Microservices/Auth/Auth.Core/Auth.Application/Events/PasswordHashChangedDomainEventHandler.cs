using Auth.Application.Abstractions.Events;
using Auth.Application.Features.LogoutAll;
using Auth.Domain.Events;
using MediatR;

namespace Auth.Application.Events
{
    public class PasswordHashChangedDomainEventHandler : IDomainEventHandler<PasswordHashChangedDomainEvent>
    {
        private readonly IPublisher _mediator;

        public PasswordHashChangedDomainEventHandler(IPublisher mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(PasswordHashChangedDomainEvent domainEvent)
        {
            await _mediator.Publish(new LogoutAllCommand(domainEvent.AggregateId));
        }
    }
}
