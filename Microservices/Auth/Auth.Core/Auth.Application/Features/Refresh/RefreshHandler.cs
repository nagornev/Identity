using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;

namespace Auth.Application.Features.Refresh
{
    public class RefreshHandler : ResultTRequestHandler<RefreshCommand, AuthDto>
    {
        private readonly IRefreshService _refreshService;

        public RefreshHandler(IRefreshService refreshService)
        {
            _refreshService = refreshService;
        }

        public override async Task<AuthDto> HandleAsync(RefreshCommand request, CancellationToken cancellation)
        {
            return await _refreshService.RefreshAsync(request.RefreshToken, request.NewPublicKey, request.Timestamp, request.Signature, cancellation = default);
        }
    }
}
