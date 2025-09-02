using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Sessions;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SessionQueryService : ISessionQueryService
    {
        private readonly IRepositoryReader<Session> _sessionRepository;

        public SessionQueryService(IRepositoryReader<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public IAsyncEnumerable<Session> FindInvalidSessionsAsyncStream(long timestamp, int unactiveWindow = 560000)
        {
            SessionByInvalidParametersSpecification specification = new SessionByInvalidParametersSpecification(timestamp, unactiveWindow);

            return _sessionRepository.AsyncStream(specification);
        }

        public IAsyncEnumerable<Session> FindSessionsByUserIdAsyncStream(Guid userId)
        {
            SessionByUserIdSpecification specification = new SessionByUserIdSpecification(userId);

            return _sessionRepository.AsyncStream(specification);
        }

        public async Task<Session> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellation = default)
        {
            SessionByIdSpecification specification = new SessionByIdSpecification(sessionId);

            return await _sessionRepository.GetAsync(specification, cancellation) ??
                   throw new SessionNotFoundApplicationException(sessionId);
        }
    }
}
