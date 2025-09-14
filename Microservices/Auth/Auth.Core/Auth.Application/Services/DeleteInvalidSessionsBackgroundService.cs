using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class DeleteInvalidSessionsBackgroundService : IDeleteInvalidSessionsBackgroundService
    {
        private readonly ISessionQueryService _sessionQueryService;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteInvalidSessionsBackgroundService(ISessionQueryService sessionQueryService,
                                                      ITimeProvider timeProvider,
                                                      IUnitOfWork unitOfWork)
        {
            _sessionQueryService = sessionQueryService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteInvalidSessionsAsync(CancellationToken cancellation = default)
        {
            long timestamp = _timeProvider.NowUnix();

            IReadOnlyCollection<Session> invalidSessions = await _sessionQueryService.FindInvalidSessionsAsync(timestamp);

            foreach (Session invalidSession in invalidSessions)
            {
                if (invalidSession.IsValidAt(timestamp))
                    continue;

                invalidSession.MarkAsDeleted();
            }

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
