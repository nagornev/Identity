using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IScopeQueryService
    {
        Task<Scope> GetScopeByIdAsync(Guid id, CancellationToken cancellation = default);

        Task<IReadOnlyCollection<Scope>> GetScopesByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellation = default);
    }
}
