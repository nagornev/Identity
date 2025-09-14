using Otp.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Features.Resend
{
    public class ResendHandler : ResultRequestHandler<ResendCommand>
    {
        private readonly IOneTimePasswordResendService _oneTimePasswordResendService;

        public ResendHandler(IOneTimePasswordResendService oneTimePasswordResendService)
        {
            _oneTimePasswordResendService = oneTimePasswordResendService;
        }

        public override async Task HandleAsync(ResendCommand request, CancellationToken cancellation)
        {
            await _oneTimePasswordResendService.ResendAsync(request.OneTimePasswordId, cancellation);
        }
    }
}
