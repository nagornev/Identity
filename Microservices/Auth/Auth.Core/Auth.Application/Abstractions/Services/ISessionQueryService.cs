using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface ISessionQueryService
    {
        IAsyncEnumerable<Session> GetSessionAsyncStreamByUserId(Guid userId);

        Task<Session> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellation = default);
    }
}
