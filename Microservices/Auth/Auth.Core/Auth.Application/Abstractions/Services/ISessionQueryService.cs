using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface ISessionQueryService
    {
        Task<IReadOnlyCollection<Session>> FindInvalidSessionsAsync(long timestamp, int unactiveWindow = 560_000, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<Session>> FindSessionsByUserIdAsync(Guid userId, CancellationToken cancellation = default);

        Task<Session> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellation = default);
    }
}
