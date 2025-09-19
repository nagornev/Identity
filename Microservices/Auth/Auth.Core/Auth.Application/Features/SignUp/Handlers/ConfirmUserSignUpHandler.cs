using Auth.Application.Abstractions.Services;
using Auth.Application.Features.SignUp.Commands;

namespace Auth.Application.Features.SignUp.Handlers
{
    public class ConfirmUserSignUpHandler : ResultRequestHandler<ConfirmUserSignUpCommand>
    {
        private readonly ISignUpConfirmService _signUpConfirmService;

        public ConfirmUserSignUpHandler(ISignUpConfirmService signUpConfirmService,
                                        ILogService logService)
            : base(logService)
        {
            _signUpConfirmService = signUpConfirmService;
        }

        public override async Task HandleAsync(ConfirmUserSignUpCommand request, CancellationToken cancellation)
        {
            await _signUpConfirmService.ConfirmAsync(request.ChannelToken, cancellation);
        }
    }
}
