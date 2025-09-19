using Auth.Application.Abstractions.Services;

namespace Auth.Application.Features.LogoutAll
{
    public class LogoutAllHandler : ResultRequestHandler<LogoutAllCommand>
    {
        private readonly ILogoutService _logoutService;

        public LogoutAllHandler(ILogoutService logoutService, 
                                ILogService logService) 
            : base(logService)
        {
            _logoutService = logoutService;
        }

        public override async Task HandleAsync(LogoutAllCommand request, CancellationToken cancellation)
        {
            await _logoutService.LogoutAllAsync(request.UserId, cancellation);
        }
    }
}
