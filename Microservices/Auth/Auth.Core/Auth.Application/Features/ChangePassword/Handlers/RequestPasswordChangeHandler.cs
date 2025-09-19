using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Application.Features.ChangePassword.Commands;

namespace Auth.Application.Features.ChangePassword.Handlers
{
    public class RequestPasswordChangeHandler : ResultTRequestHandler<RequestPasswordChangeCommand, Otp>
    {
        private readonly IPasswordChangeRequestService _passwordChangeRequestService;

        public RequestPasswordChangeHandler(IPasswordChangeRequestService passwordChangeRequestService,
                                            ILogService logService)
            : base(logService)
        {
            _passwordChangeRequestService = passwordChangeRequestService;
        }

        public override async Task<Otp> HandleAsync(RequestPasswordChangeCommand request, CancellationToken cancellation)
        {
            return await _passwordChangeRequestService.RequestAsync(request.UserId, request.OldPassword, request.NewPassword, cancellation);
        }
    }
}
