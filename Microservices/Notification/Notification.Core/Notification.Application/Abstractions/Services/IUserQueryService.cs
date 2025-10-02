using Notification.Domain.Aggregates;

namespace Notification.Application.Abstractions.Services
{
    public interface IUserQueryService
    {
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellation = default);

    }
}
