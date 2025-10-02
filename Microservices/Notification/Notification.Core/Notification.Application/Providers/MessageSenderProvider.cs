using Notification.Application.Abstractions.Providers;
using Notification.Application.Abstractions.Senders;

namespace Notification.Application.Providers
{
    public class MessageSenderProvider : IMessageSenderProvider
    {
        private readonly IReadOnlyDictionary<string, IMessageSender> _messageSenders;

        public MessageSenderProvider(IEnumerable<IMessageSender> messageSenders)
        {
            _messageSenders = messageSenders.ToDictionary(x => x.GetHandableType(), x => x);
        }

        public IMessageSender GetSender(string channelType)
        {
            return _messageSenders.TryGetValue(channelType, out var messageSender) ?
                   messageSender :
                   throw new NotSupportedException($"This channel type ({channelType}) is not supported.");
        }
    }
}
