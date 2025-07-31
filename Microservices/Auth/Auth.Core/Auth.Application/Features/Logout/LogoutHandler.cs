using Auth.Application.Abstractions.Services;

namespace Auth.Application.Features.Logout
{
    public class LogoutHandler : ResultRequestHandler<LogoutCommand>
    {
        private readonly ILogoutService _logoutService;

        public LogoutHandler(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        public override async Task HandleAsync(LogoutCommand request, CancellationToken cancellation)
        {
            await _logoutService.LogoutAsync(request.SessionId, cancellation);
        }
    }
}
