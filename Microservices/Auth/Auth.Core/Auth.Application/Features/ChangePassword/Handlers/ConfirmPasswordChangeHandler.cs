using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangePassword.Commands;

namespace Auth.Application.Features.ChangePassword.Handlers
{
    public class ConfirmPasswordChangeHandler : ResultRequestHandler<ConfirmPasswordChangeCommand>
    {
        private readonly IPasswordChangeConfirmService _passwordChangeConfirmService;

        public ConfirmPasswordChangeHandler(IPasswordChangeConfirmService passwordChangeConfirmService,
                                            ILogService logService)
            : base(logService)
        {
            _passwordChangeConfirmService = passwordChangeConfirmService;
        }

        public override async Task HandleAsync(ConfirmPasswordChangeCommand request, CancellationToken cancellation)
        {
            await _passwordChangeConfirmService.ConfirmAsync(request.OtpId, request.Otp, cancellation);
        }
    }
}
