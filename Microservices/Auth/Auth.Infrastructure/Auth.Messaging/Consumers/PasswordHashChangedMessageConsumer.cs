using Auth.Application.Abstractions.Services;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Consumers
{
    public class PasswordHashChangedMessageConsumer : IConsumer<PasswordHashChangedMessageContract>
    {
        private readonly IPasswordHashChangedEventService _passwordHashChangedEventService;

        public PasswordHashChangedMessageConsumer(IPasswordHashChangedEventService passwordHashChangedEventService)
        {
            _passwordHashChangedEventService = passwordHashChangedEventService;
        }

        public async Task Consume(ConsumeContext<PasswordHashChangedMessageContract> context)
        {
            await _passwordHashChangedEventService.HandleAsync(context.Message.UserId);
        }
    }
}
