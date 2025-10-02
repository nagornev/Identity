using Auth.Application.Abstractions.Services;
using Auth.Application.DTOs;

namespace Auth.Application.Features.Refresh
{
    public class RefreshHandler : ResultTRequestHandler<RefreshCommand, TokenPair>
    {
        private readonly IRefreshService _refreshService;

        public RefreshHandler(IRefreshService refreshService, 
                              ILogService logService) 
            : base(logService)
        {
            _refreshService = refreshService;
        }

        public override async Task<TokenPair> HandleAsync(RefreshCommand request, CancellationToken cancellation)
        {
            return await _refreshService.RefreshAsync(request.RefreshToken,
                                                      request.NewPublicKey,
                                                      request.Timestamp,
                                                      request.Signature,
                                                      cancellation);
        }
    }
}
