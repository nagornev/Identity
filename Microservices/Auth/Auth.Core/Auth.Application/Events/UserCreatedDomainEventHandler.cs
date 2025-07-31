using Auth.Application.Abstractions.Events;
using Auth.Domain.Events;

namespace Auth.Application.Events
{
    public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public Task HandleAsync(UserCreatedDomainEvent domainEvent)
        {
            //Публикация события в брокер сообщений.

            return Task.CompletedTask;
        }
    }
}
