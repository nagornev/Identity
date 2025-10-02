using DDD.Repositories;
using Notification.Application.Abstractions.Factories;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class NotificationMessageCreateService : INotificationMessageCreateService
    {
        private readonly INotificationMessageFactory _notificationMessageFactory;

        private readonly IRepositoryWriter<NotificationMessage> _notificationRepository;

        private readonly IUnitOfWork _unitOfWork;

        public NotificationMessageCreateService(INotificationMessageFactory notificationMessageFactory,
                                                IRepositoryWriter<NotificationMessage> notificationRepository,
                                                IUnitOfWork unitOfWork)
        {
            _notificationMessageFactory = notificationMessageFactory;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Guid userId, string channelType, string channelValue, string type, string text, CancellationToken cancellation = default)
        {
            NotificationMessage notificationMessage = _notificationMessageFactory.Create(userId, channelType, channelValue, type, text);

            await _notificationRepository.AddAsync(notificationMessage);
            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
