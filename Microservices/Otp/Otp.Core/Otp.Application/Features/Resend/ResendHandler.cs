using Otp.Application.Abstractions.Services;

namespace Otp.Application.Features.Resend
{
    public class ResendHandler : ResultRequestHandler<ResendCommand>
    {
        private readonly IOneTimePasswordResendService _oneTimePasswordResendService;

        public ResendHandler(IOneTimePasswordResendService oneTimePasswordResendService,
                             ILogService logService)
            : base(logService)
        {
            _oneTimePasswordResendService = oneTimePasswordResendService;
        }

        public override async Task HandleAsync(ResendCommand request, CancellationToken cancellation)
        {
            await _oneTimePasswordResendService.ResendAsync(request.OneTimePasswordId, cancellation);
        }
    }
}
