using Auth.Application.Abstractions.Services;
using Auth.Application.Features.SignUp.Commands;

namespace Auth.Application.Features.SignUp.Handlers
{
    public class RequestUserSignUpHandler : ResultRequestHandler<RequestUserSignUpCommand>
    {
        private readonly ISignUpRequestService _signUpRequestService;

        public RequestUserSignUpHandler(ISignUpRequestService signUpRequestService,
                                        ILogService logService)
            : base(logService)
        {
            _signUpRequestService = signUpRequestService;
        }

        public override async Task HandleAsync(RequestUserSignUpCommand request, CancellationToken cancellation)
        {
            await _signUpRequestService.RequestAsync(request.EmailAddress, request.PersonName, request.Password, cancellation);
        }
    }
}
