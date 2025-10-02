using DDD.Repositories;
using Notification.Application.Abstractions.Providers;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class DeleteExpiredNotificationMessageBackgroundService : IDeleteExpiredNotificationMessageBackgroundService
    {
        private readonly INotificationMessageQueryService _notificationMessageQueryService;

        private readonly IRepositoryWriter<NotificationMessage> _notificationMessageRepository;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpiredNotificationMessageBackgroundService(INotificationMessageQueryService notificationMessageQueryService,
                                                                 IRepositoryWriter<NotificationMessage> notificationMessageRepository,
                                                                 ITimeProvider timeProvider,
                                                                 IUnitOfWork unitOfWork)
        {
            _notificationMessageQueryService = notificationMessageQueryService;
            _notificationMessageRepository = notificationMessageRepository;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(CancellationToken cancellation = default)
        {
            IReadOnlyCollection<NotificationMessage> expiredNotificationMessages = await _notificationMessageQueryService.GetExpiredNotificationMessagesAsync(_timeProvider.NowUnix());

            foreach (NotificationMessage expiredNotificationMessage in expiredNotificationMessages)
            {
                expiredNotificationMessage.MarkAsDeleted();
                await _notificationMessageRepository.DeleteAsync(expiredNotificationMessage, cancellation);
            }

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
