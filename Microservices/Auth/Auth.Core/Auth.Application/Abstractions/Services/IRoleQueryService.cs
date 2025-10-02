using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IRoleQueryService
    {
        Task<IReadOnlyCollection<Role>> GetRolesByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellation = default);

        Task<Role> GetRoleByIdAsync(Guid id, CancellationToken cancellation = default);

        Task<Role> GetRoleByNameAsync(string name, CancellationToken cancellation = default);

    }
}
