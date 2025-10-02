using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangeEmailAddress.Commands;

namespace Auth.Application.Features.ChangeEmailAddress.Handlers
{
    public class ConfirmEmailAddressChangeHandler : ResultRequestHandler<ConfirmEmailAddressChangeCommand>
    {
        private readonly IEmailAddressChangeConfirmService _emalAddressChangeConfirmService;

        public ConfirmEmailAddressChangeHandler(IEmailAddressChangeConfirmService emalAddressConfirmService,
                                                ILogService logService)
            : base(logService)
        {
            _emalAddressChangeConfirmService = emalAddressConfirmService;
        }

        public override async Task HandleAsync(ConfirmEmailAddressChangeCommand request, CancellationToken cancellation)
        {
            await _emalAddressChangeConfirmService.ConfirmAsync(request.OtpId, request.Opt, cancellation);
        }
    }
}
