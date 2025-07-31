using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Sessions;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class SessionQueryService : ISessionQueryService
    {
        private readonly IRepositoryReader<Session> _sessionRepository;

        public SessionQueryService(IRepositoryReader<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public IAsyncEnumerable<Session> GetSessionAsyncStreamByUserId(Guid userId)
        {
            SessionUserIdSpecification specification = new SessionUserIdSpecification(userId);

            return _sessionRepository.AsyncStream(specification);
        }

        public async Task<Session> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellation = default)
        {
            SessionIdSpecification specification = new SessionIdSpecification(sessionId);

            return await _sessionRepository.GetAsync(specification, cancellation) ??
                   throw new SessionNotFoundApplicationException(sessionId);
        }
    }
}
