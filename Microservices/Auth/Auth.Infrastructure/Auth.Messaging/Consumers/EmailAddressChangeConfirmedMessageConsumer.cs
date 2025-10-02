using Auth.Application.Abstractions.Services;
using MassTransit;
using MessageContracts;

namespace Auth.Messaging.Consumers
{
    public class EmailAddressChangeConfirmedMessageConsumer : IConsumer<EmailAddressChangeConfirmedMessageContract>
    {
        private readonly IEmailAddressChangeConfirmedEventService _emailAddressChangeConfirmedEventService;

        public EmailAddressChangeConfirmedMessageConsumer(IEmailAddressChangeConfirmedEventService emailAddressChangeConfirmedEventService)
        {
            _emailAddressChangeConfirmedEventService = emailAddressChangeConfirmedEventService;
        }

        public async Task Consume(ConsumeContext<EmailAddressChangeConfirmedMessageContract> context)
        {
            await _emailAddressChangeConfirmedEventService.HandleAsync(context.Message.UserId, context.Message.NewEmailAddress);
        }
    }
}
