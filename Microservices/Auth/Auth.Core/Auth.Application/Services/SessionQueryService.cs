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

        public async Task<IReadOnlyCollection<Session>> FindInvalidSessionsAsync(long timestamp, int unactiveWindow = 560000, CancellationToken cancellation = default)
        {
            SessionByInvalidParametersSpecification specification = new SessionByInvalidParametersSpecification(timestamp, unactiveWindow);

            return await _sessionRepository.FindAsync(specification, cancellation);
        }

        public async Task<IReadOnlyCollection<Session>> FindSessionsByUserIdAsync(Guid userId, CancellationToken cancellation = default)
        {
            SessionByUserIdSpecification specification = new SessionByUserIdSpecification(userId);

            return await _sessionRepository.FindAsync(specification, cancellation);
        }

        public async Task<Session> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellation = default)
        {
            SessionByIdSpecification specification = new SessionByIdSpecification(sessionId);

            return await _sessionRepository.GetAsync(specification, cancellation) ??
                   throw new SessionNotFoundApplicationException(sessionId);
        }
    }
}
