using Notification.Application.Abstractions.Services;
using Notification.Domain.Consts;

namespace Notification.Application.Services
{
    public class OneTimePasswordNotificationMessageCreateService : IOneTimePasswordNotificationMessageCreateService
    {
        private readonly INotificationMessageCreateService _notificationMessageCreateService;

        public OneTimePasswordNotificationMessageCreateService(INotificationMessageCreateService notificationMessageCreateService)
        {
            _notificationMessageCreateService = notificationMessageCreateService;
        }

        public async Task CreateAsync(Guid userId, string channelType, string channelValue, string oneTimePasswordValue, CancellationToken cancellation = default)
        {
            await _notificationMessageCreateService.CreateAsync(userId,
                                                                channelType,
                                                                channelValue,
                                                                NotificationType.Otp,
                                                                oneTimePasswordValue,
                                                                cancellation);
        }
    }
}
