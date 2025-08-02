using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IScopeQueryService
    {
        Task<IReadOnlyCollection<Scope>> GetScopesByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellation = default);
    }
}
