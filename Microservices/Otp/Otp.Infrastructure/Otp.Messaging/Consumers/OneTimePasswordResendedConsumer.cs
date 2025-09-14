using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;

namespace Otp.Messaging.Consumers
{
    public class OneTimePasswordResendedConsumer : IConsumer<OneTimePasswordResendedMessageContract>
    {
        private readonly IOneTimePasswordCreatedEventService _oneTimePasswordCreatedEventService;

        public OneTimePasswordResendedConsumer(IOneTimePasswordCreatedEventService oneTimePasswordCreatedEventService)
        {
            _oneTimePasswordCreatedEventService = oneTimePasswordCreatedEventService;
        }

        public async Task Consume(ConsumeContext<OneTimePasswordResendedMessageContract> context)
        {
            await _oneTimePasswordCreatedEventService.HandleAsync(context.Message.OneTimePasswordId, context.CancellationToken);
        }
    }
}
