using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;

namespace Otp.Messaging.Consumers
{
    public class UserActivatedConsumer : IConsumer<UserActivatedMessageContract>
    {
        private readonly IUserActivatedEventService _userActivatedEventService;

        public UserActivatedConsumer(IUserActivatedEventService userActivatedEventService)
        {
            _userActivatedEventService = userActivatedEventService;
        }

        public async Task Consume(ConsumeContext<UserActivatedMessageContract> context)
        {
            await _userActivatedEventService.HandleAsync(context.Message.UserId,
                                                         context.Message.EmailAddress);
        }
    }
}
