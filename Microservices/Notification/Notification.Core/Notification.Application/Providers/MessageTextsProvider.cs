using Notification.Application.Abstractions.Providers;

namespace Notification.Application.Providers
{
    public class MessageTextsProvider : IMessageTextsProvider
    {
        private readonly IReadOnlyDictionary<string, IMessageTextProvider> _messageTextProviders;

        public MessageTextsProvider(IEnumerable<IMessageTextProvider> messageTextProviders)
        {
            _messageTextProviders = messageTextProviders.ToDictionary(x => x.GetHandableType(), x => x);
        }

        public string GetText(string notificationType, string text)
        {
            return _messageTextProviders.TryGetValue(notificationType, out var messageTextProvider) ?
                    messageTextProvider.GetText(text) :
                    throw new NotSupportedException($"This notification type ({notificationType}) is not supported.");
        }
    }
}
