using DDD.Repositories;
using Notification.Application.Abstractions.Factories;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class UserActivatedEventService : IUserActivatedEventService
    {
        private readonly IUserFactory _userFactory;

        private readonly IRepositoryWriter<User> _userRepository;

        private readonly IPendingNotificationMessageQueryService _pendingNotificationMessageQueryService;

        private readonly IRepositoryWriter<PendingNotificationMessage> _pendingNotificationMessageRepository;

        private readonly IRepositoryWriter<NotificationMessage> _notificationMessageRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UserActivatedEventService(IUserFactory userFactory,
                                         IRepositoryWriter<User> userRepository,
                                         IPendingNotificationMessageQueryService pendingNotificationMessageQueryService,
                                         IRepositoryWriter<PendingNotificationMessage> pendingNotificationMessageRepository,
                                         IRepositoryWriter<NotificationMessage> notificationMessageRepository,
                                         IUnitOfWork unitOfWork)
        {
            _userFactory = userFactory;
            _userRepository = userRepository;
            _pendingNotificationMessageQueryService = pendingNotificationMessageQueryService;
            _pendingNotificationMessageRepository = pendingNotificationMessageRepository;
            _notificationMessageRepository = notificationMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(Guid userId, string email, CancellationToken cancellation = default)
        {
            User user = _userFactory.Create(userId, email);
            await _userRepository.AddAsync(user);

            IReadOnlyCollection<PendingNotificationMessage> pendingNotificationMessages = await _pendingNotificationMessageQueryService.GetPendingNotificationMessagesByUserIdAsync(userId, cancellation);

            foreach (PendingNotificationMessage pendingNotificationMessage in pendingNotificationMessages)
            {
                NotificationMessage notificationMessage = pendingNotificationMessage.ToNotificationMessage();

                await _pendingNotificationMessageRepository.DeleteAsync(pendingNotificationMessage);
                await _notificationMessageRepository.AddAsync(notificationMessage);
            }

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
