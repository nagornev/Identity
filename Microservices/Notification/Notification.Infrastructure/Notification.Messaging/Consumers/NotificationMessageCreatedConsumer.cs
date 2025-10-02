using MassTransit;
using MessageContracts;
using Notification.Application.Abstractions.Services;

namespace Notification.Messaging.Consumers
{
    public class NotificationMessageCreatedConsumer : IConsumer<NotificationMessageCreatedMessageContract>
    {
        private readonly INotificationMessageSendService _notificationMessageSendService;

        public NotificationMessageCreatedConsumer(INotificationMessageSendService notificationMessageSendService)
        {
            _notificationMessageSendService = notificationMessageSendService;
        }


        public async Task Consume(ConsumeContext<NotificationMessageCreatedMessageContract> context)
        {
            await _notificationMessageSendService.SendAsync(context.Message.NotificationMessageId,
                                                            context.CancellationToken);
        }
    }
}
