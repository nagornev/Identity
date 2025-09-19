using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;
using Auth.Application.Features.SignIn.Queries;

namespace Auth.Application.Features.SignIn.Handlers
{
    public class ConfirmUserSignInHandler : ResultTRequestHandler<ConfirmUserSignInQuery, TokenPair>
    {
        private readonly ISignInConfirmService _signInConfrmService;

        public ConfirmUserSignInHandler(ISignInConfirmService signInConfrimService, 
                                        ILogService logService)
            : base(logService)
        {
            _signInConfrmService = signInConfrimService;
        }

        public override async Task<TokenPair> HandleAsync(ConfirmUserSignInQuery request, CancellationToken cancellation)
        {
            return await _signInConfrmService.ConfirmAsync(request.OtpId, request.Otp, cancellation);
        }
    }
}
