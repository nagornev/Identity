using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Messaging.Consumers
{
    public class OneTimePasswordCreatedConsumer : IConsumer<OneTimePasswordCreatedMessageContract>
    {
        private readonly IOneTimePasswordCreatedEventService _oneTimePasswordCreatedEventService;

        public OneTimePasswordCreatedConsumer(IOneTimePasswordCreatedEventService oneTimePasswordCreatedEventService)
        {
            _oneTimePasswordCreatedEventService = oneTimePasswordCreatedEventService;
        }

        public async Task Consume(ConsumeContext<OneTimePasswordCreatedMessageContract> context)
        {
            await _oneTimePasswordCreatedEventService.HandleAsync(context.Message.OneTimePasswordId, context.CancellationToken);
        }
    }
}
