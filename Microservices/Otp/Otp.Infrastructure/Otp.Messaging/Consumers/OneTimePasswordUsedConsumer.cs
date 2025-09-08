using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;

namespace Otp.Messaging.Consumers
{
    public class OneTimePasswordUsedConsumer : IConsumer<OneTimePasswordUsedMessageContract>
    {
        private readonly IOneTimePasswordUsedEventService _timePasswordUsedEventService;

        public OneTimePasswordUsedConsumer(IOneTimePasswordUsedEventService timePasswordUsedEventService)
        {
            _timePasswordUsedEventService = timePasswordUsedEventService;
        }

        public async Task Consume(ConsumeContext<OneTimePasswordUsedMessageContract> context)
        {
            await _timePasswordUsedEventService.HandleAsync(context.Message.OneTimePasswordId, context.CancellationToken);
        }
    }
}
