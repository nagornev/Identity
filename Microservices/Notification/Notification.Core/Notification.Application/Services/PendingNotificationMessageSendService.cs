using Notification.Application.Abstractions.Providers;
using Notification.Application.Abstractions.Senders;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class PendingNotificationMessageSendService : IPendingNotificationMessageSendService
    {
        private readonly IPendingNotificationMessageQueryService _pendingNotificationMessageQueryService;

        private readonly IMessageTextsProvider _messageTextsProvider;

        private readonly IMessageSenderProvider _messageSenderProvider;

        public PendingNotificationMessageSendService(IPendingNotificationMessageQueryService pendingNotificationMessageQueryService,
                                                     IMessageTextsProvider messageTextsProvider,
                                                     IMessageSenderProvider messageSenderProvider)
        {
            _pendingNotificationMessageQueryService = pendingNotificationMessageQueryService;
            _messageTextsProvider = messageTextsProvider;
            _messageSenderProvider = messageSenderProvider;
        }

        public async Task SendAsync(Guid pendingNotificationMessageId, CancellationToken cancellation = default)
        {
            PendingNotificationMessage pendingNotificationMessage = await _pendingNotificationMessageQueryService.GetPendingNotificationMessageByIdAsync(pendingNotificationMessageId, cancellation);

            string text = _messageTextsProvider.GetText(pendingNotificationMessage.Type, pendingNotificationMessage.Text);
            IMessageSender messageSender = _messageSenderProvider.GetSender(pendingNotificationMessage.Channel.Type);

            await messageSender.SendAsync(pendingNotificationMessage.Channel.Value, text);
        }
    }
}
