using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class LogoutService : ILogoutService
    {
        private readonly ISessionQueryService _sessionQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public LogoutService(ISessionQueryService sessionQueryService,
                             IUnitOfWork unitOfWork)
        {
            _sessionQueryService = sessionQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task LogoutAllAsync(Guid userId, CancellationToken cancellation = default)
        {
            IAsyncEnumerable<Session> sessions = _sessionQueryService.GetSessionAsyncStreamByUserId(userId);

            await foreach (Session session in sessions.WithCancellation(cancellation))
            {
                session.CloseSession();
            }

            await _unitOfWork.SaveAsync(cancellation);
        }

        public async Task LogoutAsync(Guid sessionId, CancellationToken cancellation = default)
        {
            Session session = await _sessionQueryService.GetSessionByIdAsync(sessionId, cancellation);
            session.CloseSession();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
