using Auth.Application.Abstractions.Services;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Consumers
{
    public class UserCreatedMessageConsumer : IConsumer<UserCreatedMessageContract>
    {
        private readonly IUserCreatedEventService _userCreatedEventService;

        public UserCreatedMessageConsumer(IUserCreatedEventService userCreatedEventService)
        {
            _userCreatedEventService = userCreatedEventService;
        }

        public async Task Consume(ConsumeContext<UserCreatedMessageContract> context)
        {
            await _userCreatedEventService.HandleAsync(context.Message.UserId, context.Message.EmailAddress);
        }
    }
}
