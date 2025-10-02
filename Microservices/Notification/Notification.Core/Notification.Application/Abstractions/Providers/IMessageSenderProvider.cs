using Notification.Application.Abstractions.Senders;

namespace Notification.Application.Abstractions.Providers
{
    public interface IMessageSenderProvider
    {
        IMessageSender GetSender(string channelType);
    }
}
