using Notification.Application.Abstractions.Providers;
using Notification.Domain.Consts;

namespace Notification.Application.Providers
{
    public class ChannelMessageTextProvider : IMessageTextProvider
    {
        public string GetHandableType()
        {
            return NotificationType.Channel;
        }

        public string GetText(string url)
        {
            return "To confirm the channel, follow on this link.\n" +
                   $"{url}";
        }
    }
}
