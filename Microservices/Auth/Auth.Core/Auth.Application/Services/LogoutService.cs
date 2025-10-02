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
            IReadOnlyCollection<Session> sessions = await _sessionQueryService.FindSessionsByUserIdAsync(userId);

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
