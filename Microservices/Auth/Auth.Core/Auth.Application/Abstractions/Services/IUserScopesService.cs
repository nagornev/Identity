using Auth.Domain.Aggregates;
using Auth.Domain.ValueObjects;

namespace Auth.Application.Abstractions.Services
{
    public interface IUserScopesService
    {
        Task<IReadOnlyCollection<Scope>> GetUserScopesAsync(User user, Audience audience, CancellationToken cancellation = default);
    }
}
