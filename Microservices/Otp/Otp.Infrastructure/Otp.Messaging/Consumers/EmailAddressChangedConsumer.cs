using MassTransit;
using MessageContracts;
using Otp.Application.Abstractions.Services;

namespace Otp.Messaging.Consumers
{
    public class EmailAddressChangedConsumer : IConsumer<EmailAddressChangedMessageContract>
    {
        private readonly IEmailAddressChangedEventService _emailAddressChangedEventService;

        public EmailAddressChangedConsumer(IEmailAddressChangedEventService emailAddressChangedEventService)
        {
            _emailAddressChangedEventService = emailAddressChangedEventService;
        }

        public async Task Consume(ConsumeContext<EmailAddressChangedMessageContract> context)
        {
            await _emailAddressChangedEventService.HandleAsync(context.Message.UserId, context.Message.EmailAddress, context.CancellationToken);
        }
    }
}
