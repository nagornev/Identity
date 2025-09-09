using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;

namespace Otp.Messaging.Consumers
{
    public class OneTimePasswordCreationRequestConsumer : IConsumer<OneTimePasswordCreationRequest>
    {
        private readonly IOneTimeCreateService _oneTimeCreateService;

        public OneTimePasswordCreationRequestConsumer(IOneTimeCreateService oneTimeCreateService)
        {
            _oneTimeCreateService = oneTimeCreateService;
        }

        public async Task Consume(ConsumeContext<OneTimePasswordCreationRequest> context)
        {
            Guid oneTimePasswordId = await _oneTimeCreateService.CreateAsync(
                     context.Message.Tag,
                     context.Message.Subject,
                     context.Message.Payload,
                     context.CancellationToken);

            await context.RespondAsync(new OneTimePasswordCreationCompleted(oneTimePasswordId));
        }
    }
}
