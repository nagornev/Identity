using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IUserInitializeService
    {
        Task Initialize(User user, CancellationToken cancellation = default);
    }
}
