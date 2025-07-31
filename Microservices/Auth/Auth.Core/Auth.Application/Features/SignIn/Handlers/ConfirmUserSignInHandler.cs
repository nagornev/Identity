using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Application.Features.SignIn.Queries;

namespace Auth.Application.Features.SignIn.Handlers
{
    public class ConfirmUserSignInHandler : ResultTRequestHandler<ConfirmUserSignInQuery, AuthDto>
    {
        private readonly ISignInConfirmService _signInConfrmService;

        public ConfirmUserSignInHandler(ISignInConfirmService signInConfrimService)
        {
            _signInConfrmService = signInConfrimService;
        }

        public override async Task<AuthDto> HandleAsync(ConfirmUserSignInQuery request, CancellationToken cancellation)
        {
            return await _signInConfrmService.ConfirmAsync(request.OtpToken, request.Otp, request.PublicKey, request.Device, request.IpAddress, cancellation);
        }
    }
}
