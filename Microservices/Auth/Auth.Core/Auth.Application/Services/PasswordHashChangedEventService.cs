using Auth.Application.Abstractions.Services;

namespace Auth.Application.Services
{
    public class PasswordHashChangedEventService : IPasswordHashChangedEventService
    {
        private readonly ILogoutService _logoutService;

        public PasswordHashChangedEventService(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        public async Task HandleAsync(Guid userId, CancellationToken cancellation = default)
        {
            await _logoutService.LogoutAllAsync(userId, cancellation);
        }
    }
}
