using DDD.Repositories;
using Notification.Application.Abstractions.Providers;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class DeleteExpiredPendingNotificationMessageBackgroundService : IDeleteExpiredPendingNotificationMessageBackgroundService
    {
        private readonly IPendingNotificationMessageQueryService _pendingNotificationMessageQueryService;

        private readonly IRepositoryWriter<PendingNotificationMessage> _pendingNotificationMessageRepository;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpiredPendingNotificationMessageBackgroundService(IPendingNotificationMessageQueryService pendingNotificationMessageQueryService,
                                                                 IRepositoryWriter<PendingNotificationMessage> pendingNotificationMessageRepository,
                                                                 ITimeProvider timeProvider,
                                                                 IUnitOfWork unitOfWork)
        {
            _pendingNotificationMessageQueryService = pendingNotificationMessageQueryService;
            _pendingNotificationMessageRepository = pendingNotificationMessageRepository;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(CancellationToken cancellation = default)
        {
            IReadOnlyCollection<PendingNotificationMessage> expiredPendingNotificationMessages = await _pendingNotificationMessageQueryService.GetExpiredPendingNotificaitonMessagesAsync(_timeProvider.NowUnix(), cancellation);

            foreach (PendingNotificationMessage expiredPendingNotificationMessage in expiredPendingNotificationMessages)
            {
                expiredPendingNotificationMessage.MarkAsDeleted();
                await _pendingNotificationMessageRepository.DeleteAsync(expiredPendingNotificationMessage);
            }

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
