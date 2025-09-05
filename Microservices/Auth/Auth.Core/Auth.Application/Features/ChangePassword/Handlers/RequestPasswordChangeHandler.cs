using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangePassword.Commands;

namespace Auth.Application.Features.ChangePassword.Handlers
{
    public class RequestPasswordChangeHandler : ResultTRequestHandler<RequestPasswordChangeCommand, Guid>
    {
        private readonly IPasswordChangeRequestService _passwordChangeRequestService;

        public RequestPasswordChangeHandler(IPasswordChangeRequestService passwordChangeRequestService)
        {
            _passwordChangeRequestService = passwordChangeRequestService;
        }

        public override async Task<Guid> HandleAsync(RequestPasswordChangeCommand request, CancellationToken cancellation)
        {
            return await _passwordChangeRequestService.RequestAsync(request.UserId, request.OldPassword, request.NewPassword, cancellation);
        }
    }
}
