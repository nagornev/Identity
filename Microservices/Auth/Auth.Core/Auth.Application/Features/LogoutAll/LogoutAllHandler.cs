using Auth.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.LogoutAll
{
    public class LogoutAllHandler : ResultRequestHandler<LogoutAllCommand>
    {
        private readonly ILogoutService _logoutService;

        public LogoutAllHandler(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        public override async Task HandleAsync(LogoutAllCommand request, CancellationToken cancellation)
        {
            await _logoutService.LogoutAllAsync(request.UserId, cancellation);
        }
    }
}
