using DDD.Repositories;
using Notification.Application.Abstractions.Services;
using Notification.Domain.Aggregates;

namespace Notification.Application.Services
{
    public class EmailAddressChangedEventService : IEmailAddressChangedEventService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public EmailAddressChangedEventService(IUserQueryService userQueryService,
                                               IUnitOfWork unitOfWork)
        {
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(Guid userId, string email, CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByIdAsync(userId, cancellation);
            user.ChangeEmailAddress(email);

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
