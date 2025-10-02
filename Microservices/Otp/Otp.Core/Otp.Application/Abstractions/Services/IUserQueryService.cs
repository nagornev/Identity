using Otp.Domain.Aggregates;

namespace Otp.Application.Abstractions.Services
{
    public interface IUserQueryService
    {
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellation = default);

    }
}
