using Notification.Application.Abstractions.Providers;
using Notification.Domain.Consts;

namespace Notification.Application.Providers
{
    public class ActivateMessageTextProvider : IMessageTextProvider
    {
        public string GetHandableType()
        {
            return NotificationType.Activate;
        }

        public string GetText(string url)
        {
            return "To activate your account, follow this link.\n" +
                   $"{url}";
        }
    }
}
