using Auth.Application.Abstractions.Services;

namespace Auth.Application.Features.Refresh
{
    public class RefreshHandler : ResultTRequestHandler<RefreshCommand, DTOs.AuthTokens>
    {
        private readonly IRefreshService _refreshService;

        public RefreshHandler(IRefreshService refreshService)
        {
            _refreshService = refreshService;
        }

        public override async Task<DTOs.AuthTokens> HandleAsync(RefreshCommand request, CancellationToken cancellation)
        {
            return await _refreshService.RefreshAsync(request.RefreshToken,
                                                      request.NewPublicKey,
                                                      request.Timestamp,
                                                      request.Signature,
                                                      request.Device,
                                                      request.IpAddress,
                                                      cancellation);
        }
    }
}
