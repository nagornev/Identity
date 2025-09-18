using MassTransit;
using MessageContracts;
using Notification.Application.Abstractions.Services;

namespace Notification.Messaging.Consumers
{
    public class PendingNotificationMessageCreatedConsumer : IConsumer<PendingNotificationMessageCreatedMessageContract>
    {
        private readonly IPendingNotificationMessageSendService _pendingNotificationMessageSendService;

        public PendingNotificationMessageCreatedConsumer(IPendingNotificationMessageSendService pendingNotificationMessageSendService)
        {
            _pendingNotificationMessageSendService = pendingNotificationMessageSendService;
        }

        public async Task Consume(ConsumeContext<PendingNotificationMessageCreatedMessageContract> context)
        {
            await _pendingNotificationMessageSendService.SendAsync(context.Message.PendingNotificationMessageId,
                                                                   context.CancellationToken);
        }
    }
}
