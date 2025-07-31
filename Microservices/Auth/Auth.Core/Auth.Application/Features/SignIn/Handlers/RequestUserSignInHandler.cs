using Auth.Application.Abstractions.Services;
using Auth.Application.Features.SignIn.Queries;

namespace Auth.Application.Features.SignIn.Handlers
{
    public class RequestUserSignInHandler : ResultTRequestHandler<RequestUserSignInQuery, string>
    {
        private readonly ISignInRequestService _signInRequestService;

        public RequestUserSignInHandler(ISignInRequestService signInRequestService)
        {
            _signInRequestService = signInRequestService;
        }

        public override async Task<string> HandleAsync(RequestUserSignInQuery request, CancellationToken cancellation)
        {
            return await _signInRequestService.RequestAsync(request.EmailAddress, request.Password, cancellation);
        }
    }
}
