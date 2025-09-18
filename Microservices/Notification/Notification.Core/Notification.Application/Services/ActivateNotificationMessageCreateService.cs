using Notification.Application.Abstractions.Services;
using Notification.Domain.Consts;

namespace Notification.Application.Services
{
    public class ActivateNotificationMessageCreateService : IActivateNotificationMessageCreateService
    {
        private readonly IPendingNotificationMessageCreateService _pendingNotificationMessageCreateService;

        public ActivateNotificationMessageCreateService(IPendingNotificationMessageCreateService pendingNotificationMessageCreateService)
        {
            _pendingNotificationMessageCreateService = pendingNotificationMessageCreateService;
        }

        public async Task CreateAsync(Guid userId, string channelType, string channelValue, string url, CancellationToken cancellation = default)
        {
            await _pendingNotificationMessageCreateService.CreateAsync(userId,
                                                                       channelType,
                                                                       channelValue,
                                                                       NotificationType.Activate,
                                                                       url,
                                                                       cancellation);
        }
    }
}
