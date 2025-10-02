using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Services
{
    public interface IUserCreateService
    {
        Task<User> CreateAsync(string emailAddress, string personName, string password, CancellationToken cancellation = default);
    }
}
