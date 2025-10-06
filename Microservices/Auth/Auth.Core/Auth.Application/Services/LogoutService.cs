using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class LogoutService : ILogoutService
    {
        private readonly ISessionQueryService _sessionQueryService;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public LogoutService(ISessionQueryService sessionQueryService,
                             ITimeProvider timeProvider,
                             IUnitOfWork unitOfWork)
        {
            _sessionQueryService = sessionQueryService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task LogoutAllAsync(Guid userId, CancellationToken cancellation = default)
        {
            IReadOnlyCollection<Session> sessions = await _sessionQueryService.FindValidSessionsByUserIdAsync(userId, _timeProvider.NowUnix());

            foreach (Session session in sessions)
            {
                session.Close();
            }

            await _unitOfWork.SaveAsync(cancellation);
        }

        public async Task LogoutAsync(Guid sessionId, CancellationToken cancellation = default)
        {
            Session session = await _sessionQueryService.GetSessionByIdAsync(sessionId, cancellation);
            session.Close();

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
