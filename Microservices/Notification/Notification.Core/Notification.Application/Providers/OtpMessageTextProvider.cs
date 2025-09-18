using Notification.Application.Abstractions.Providers;
using Notification.Domain.Consts;

namespace Notification.Application.Providers
{
    public class OtpMessageTextProvider : IMessageTextProvider
    {
        public string GetHandableType()
        {
            return NotificationType.Otp;
        }

        public string GetText(string oneTimePasswordValue)
        {
            return $"Your one time password code: {oneTimePasswordValue}";
        }
    }
}
