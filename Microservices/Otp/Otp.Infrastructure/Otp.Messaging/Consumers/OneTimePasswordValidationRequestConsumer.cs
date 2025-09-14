using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;
using Otp.Application.DTOs;

namespace Otp.Messaging.Consumers
{
    public class OneTimePasswordValidationRequestConsumer : IConsumer<OneTimePasswordValidationRequest>
    {
        private readonly IOneTimePasswordValidationService _oneTimePasswordValidationService;

        public OneTimePasswordValidationRequestConsumer(IOneTimePasswordValidationService oneTimePasswordValidationService)
        {
            _oneTimePasswordValidationService = oneTimePasswordValidationService;
        }

        public async Task Consume(ConsumeContext<OneTimePasswordValidationRequest> context)
        {
            OneTimePasswordValidation oneTimePasswordValidation = await _oneTimePasswordValidationService.ValidateAsync(context.Message.OneTimePasswordId,
                                                                                                                        context.Message.OneTimePasswordValue,
                                                                                                                        context.Message.Tag,
                                                                                                                        context.CancellationToken);

            await context.RespondAsync(new OneTimePasswordValidationCompleted(oneTimePasswordValidation.IsValid,
                                                                              oneTimePasswordValidation.UserId,
                                                                              oneTimePasswordValidation.Payload));
        }
    }
}
