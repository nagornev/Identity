using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangeEmailAddress.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.ChangeEmailAddress.Handlers
{
    public class EmailAddressUpdateHandler : ResultRequestHandler<EmailAddressUpdateCommand>
    {
        private readonly IEmailAddressUpdateService _emailAddressUpdateService;

        public EmailAddressUpdateHandler(IEmailAddressUpdateService emailAddressUpdateService)
        {
            _emailAddressUpdateService = emailAddressUpdateService;
        }

        public override async Task HandleAsync(EmailAddressUpdateCommand request, CancellationToken cancellation)
        {
            await _emailAddressUpdateService.UpdateAsync(request.EmailToken, cancellation);
        }
    }
}
