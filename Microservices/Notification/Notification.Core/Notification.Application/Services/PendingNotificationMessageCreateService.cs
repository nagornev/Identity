using DDD.Repositories;
using Notification.Application.Abstractions.Factories;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class PendingNotificationMessageCreateService : IPendingNotificationMessageCreateService
    {
        private readonly IPendingNotificationMessageFactory _pendingNotificationMessageFactory;

        private readonly IRepositoryWriter<PendingNotificationMessage> _pendingNotificationMessageRepository;

        private readonly IUnitOfWork _unitOfWork;

        public PendingNotificationMessageCreateService(IPendingNotificationMessageFactory pendingNotificationMessageFactory,
                                                       IRepositoryWriter<PendingNotificationMessage> pendingNotificationMessageRepository,
                                                       IUnitOfWork unitOfWork)
        {
            _pendingNotificationMessageFactory = pendingNotificationMessageFactory;
            _pendingNotificationMessageRepository = pendingNotificationMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Guid userId, string channelType, string channelValue, string type, string text, CancellationToken cancellation = default)
        {
            PendingNotificationMessage pendingNotificationMessage = _pendingNotificationMessageFactory.Create(userId, channelType, channelValue, type, text);

            await _pendingNotificationMessageRepository.AddAsync(pendingNotificationMessage);
            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
