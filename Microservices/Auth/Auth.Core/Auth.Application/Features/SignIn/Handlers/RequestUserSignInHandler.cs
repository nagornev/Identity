using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Application.Features.SignIn.Queries;

namespace Auth.Application.Features.SignIn.Handlers
{
    public class RequestUserSignInHandler : ResultTRequestHandler<RequestUserSignInQuery, Otp>
    {
        private readonly ISignInRequestService _signInRequestService;

        public RequestUserSignInHandler(ISignInRequestService signInRequestService,
                                        ILogService logService)
            : base(logService)
        {
            _signInRequestService = signInRequestService;
        }

        public override async Task<Otp> HandleAsync(RequestUserSignInQuery request, CancellationToken cancellation)
        {
            return await _signInRequestService.RequestAsync(request.EmailAddress, request.Password, request.Audience, request.PublicKey, request.RequestContext, cancellation);
        }
    }
}
