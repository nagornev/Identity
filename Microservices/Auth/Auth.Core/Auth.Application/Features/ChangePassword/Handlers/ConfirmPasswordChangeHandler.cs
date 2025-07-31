using Auth.Application.Abstractions.Services;
using Auth.Application.Features.ChangePassword.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Features.ChangePassword.Handlers
{
    public class ConfirmPasswordChangeHandler : ResultRequestHandler<ConfirmPasswordChangeCommand>
    {
        private readonly IPasswordChangeConfirmService _passwordChangeConfirmService;

        public ConfirmPasswordChangeHandler(IPasswordChangeConfirmService passwordChangeConfirmService)
        {
            _passwordChangeConfirmService = passwordChangeConfirmService;
        }

        public override async Task HandleAsync(ConfirmPasswordChangeCommand request, CancellationToken cancellation)
        {
            await _passwordChangeConfirmService.ConfirmAsync(request.OtpToken, request.Otp, cancellation);
        }
    }
}
