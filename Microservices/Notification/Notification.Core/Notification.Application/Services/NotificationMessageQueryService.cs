using DDD.Repositories;
using Notification.Application.Abstractions.Services;
using Notification.Application.Exceptions.Applications.Notifications;
using Notification.Domain.Aggregates;
using Notification.Domain.Specifications;

namespace Notification.Application.Services
{
    public class NotificationMessageQueryService : INotificationMessageQueryService
    {
        private readonly IRepositoryReader<NotificationMessage> _notificationMessageRepository;

        public NotificationMessageQueryService(IRepositoryReader<NotificationMessage> notificationMessageRepository)
        {
            _notificationMessageRepository = notificationMessageRepository;
        }

        public async Task<NotificationMessage> GetNotificationMessageByIdAsync(Guid notificationMessageId, CancellationToken cancellation = default)
        {
            NotificationMessageByIdSpecification specification = new NotificationMessageByIdSpecification(notificationMessageId);

            return await _notificationMessageRepository.GetAsync(specification, cancellation) ??
                   throw new NotificationMessageNotFoundApplicationException(notificationMessageId);
        }

        public async Task<IReadOnlyCollection<NotificationMessage>> GetExpiredNotificationMessagesAsync(long timestamp, CancellationToken cancellation = default)
        {
            NotificationMessageByExpiredBeforeSpecificaiton specificaiton = new NotificationMessageByExpiredBeforeSpecificaiton(timestamp);

            return await _notificationMessageRepository.FindAsync(specificaiton, cancellation);
        }

    }
}
