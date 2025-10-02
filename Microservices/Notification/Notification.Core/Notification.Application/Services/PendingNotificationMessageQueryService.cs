using DDD.Repositories;
using Notification.Application.Abstractions.Services;
using Notification.Application.Exceptions.Applications.Notifications;
using Notification.Domain.Aggregates;
using Notification.Domain.Specifications;

namespace Notification.Application.Services
{
    public class PendingNotificationMessageQueryService : IPendingNotificationMessageQueryService
    {
        private readonly IRepositoryReader<PendingNotificationMessage> _pendingNotificationMessageRepository;

        public PendingNotificationMessageQueryService(IRepositoryReader<PendingNotificationMessage> pendingNotificationMessageRepository)
        {
            _pendingNotificationMessageRepository = pendingNotificationMessageRepository;
        }



        public async Task<PendingNotificationMessage> GetPendingNotificationMessageByIdAsync(Guid pendingNotificationMessageId, CancellationToken cancellation = default)
        {
            PendingNotificationMessageByIdSpecification specification = new PendingNotificationMessageByIdSpecification(pendingNotificationMessageId);

            return await _pendingNotificationMessageRepository.GetAsync(specification, cancellation) ??
                   throw new PendingNotificationMessageNotFoundApplicationException(pendingNotificationMessageId);
        }

        public async Task<IReadOnlyCollection<PendingNotificationMessage>> GetPendingNotificationMessagesByUserIdAsync(Guid userId, CancellationToken cancellation = default)
        {
            PendingNotificationMessageByUserIdSpecification specification = new PendingNotificationMessageByUserIdSpecification(userId);

            return await _pendingNotificationMessageRepository.FindAsync(specification, cancellation);
        }

        public async Task<IReadOnlyCollection<PendingNotificationMessage>> GetExpiredPendingNotificaitonMessagesAsync(long timestamp, CancellationToken cancellation = default)
        {
            PendingNotificationMessageByExpiredBeforeSpecification specification = new PendingNotificationMessageByExpiredBeforeSpecification(timestamp);

            return await _pendingNotificationMessageRepository.FindAsync(specification, cancellation);
        }
    }
}
