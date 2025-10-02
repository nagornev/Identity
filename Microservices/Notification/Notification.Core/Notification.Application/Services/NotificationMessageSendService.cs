using Notification.Application.Abstractions.Providers;
using Notification.Application.Abstractions.Senders;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class NotificationMessageSendService : INotificationMessageSendService
    {
        private readonly INotificationMessageQueryService _notificationMessageQueryService;

        private readonly IMessageTextsProvider _messageTextsProvider;

        private readonly IMessageSenderProvider _messageSenderProvider;

        public NotificationMessageSendService(INotificationMessageQueryService notificationMessageQueryService,
                                              IMessageTextsProvider messageTextsProvider,
                                              IMessageSenderProvider messageSenderProvider)
        {
            _notificationMessageQueryService = notificationMessageQueryService;
            _messageTextsProvider = messageTextsProvider;
            _messageSenderProvider = messageSenderProvider;
        }

        public async Task SendAsync(Guid notificationMessageId, CancellationToken cancellation = default)
        {
            NotificationMessage notificationMessage = await _notificationMessageQueryService.GetNotificationMessageByIdAsync(notificationMessageId, cancellation);

            string text = _messageTextsProvider.GetText(notificationMessage.Type, notificationMessage.Text);
            IMessageSender messageSender = _messageSenderProvider.GetSender(notificationMessage.Channel.Type);

            await messageSender.SendAsync(notificationMessage.Channel.Value, text);
        }
    }
}
