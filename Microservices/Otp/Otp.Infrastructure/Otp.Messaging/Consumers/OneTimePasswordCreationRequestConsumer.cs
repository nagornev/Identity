using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;
using Otp.Application.DTOs;

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
            OneTimePasswordCreation oneTimePasswordCreation = await _oneTimeCreateService.CreateAsync(context.Message.UserId,
                                                                                                      context.Message.Tag,
                                                                                                      context.Message.Payload,
                                                                                                      context.CancellationToken);

            await context.RespondAsync(new OneTimePasswordCreationCompleted(oneTimePasswordCreation.OneTimePasswordId,
                                                                            oneTimePasswordCreation.Type,
                                                                            oneTimePasswordCreation.Channel,
                                                                            oneTimePasswordCreation.ExpiresAt));
        }
    }
}
