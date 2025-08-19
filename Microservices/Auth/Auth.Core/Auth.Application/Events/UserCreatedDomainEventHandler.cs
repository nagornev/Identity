using Auth.Application.Abstractions.Events;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Application.Events
{
    public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IPublishMessageService _publishMessageService;

        public UserCreatedDomainEventHandler(IPublishMessageService publishMessageService)
        {
            _publishMessageService = publishMessageService;
        }

        public async Task HandleAsync(UserCreatedDomainEvent domainEvent)
        {
            UserCreatedMessageContract messageContract = new UserCreatedMessageContract(domainEvent.AggregateId);

            await _publishMessageService.PublishAsync(messageContract);
        }
    }
}
