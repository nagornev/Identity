using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangeEmailAddress.Commands;

namespace Auth.Application.Features.ChangeEmailAddress.Handlers
{
    public class EmailAddressUpdateHandler : ResultRequestHandler<EmailAddressUpdateCommand>
    {
        private readonly IEmailAddressUpdateService _emailAddressUpdateService;

        public EmailAddressUpdateHandler(IEmailAddressUpdateService emailAddressUpdateService,
                                         ILogService logService)
            : base(logService)
        {
            _emailAddressUpdateService = emailAddressUpdateService;
        }

        public override async Task HandleAsync(EmailAddressUpdateCommand request, CancellationToken cancellation)
        {
            await _emailAddressUpdateService.UpdateAsync(request.EmailToken, cancellation);
        }
    }
}
