using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IUserQueryService
    {
        Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellation = default);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellation = default);

        Task<bool> IsUserAlreadyExistsAsync(string email, CancellationToken cancellation = default);

        IAsyncEnumerable<User> FindUnactivatedUsersAsyncStream(long timestamp);
    }
}
