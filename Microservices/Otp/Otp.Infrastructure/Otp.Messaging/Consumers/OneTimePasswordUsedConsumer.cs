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
