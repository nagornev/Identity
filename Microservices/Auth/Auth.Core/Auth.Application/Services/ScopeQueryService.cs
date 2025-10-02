using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Scopes;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class ScopeQueryService : IScopeQueryService
    {
        private readonly IRepositoryReader<Scope> _scopeRepository;

        public ScopeQueryService(IRepositoryReader<Scope> scopeRepository)
        {
            _scopeRepository = scopeRepository;
        }

        public async Task<Scope> GetScopeByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            ScopeByIdSpecification specification = new ScopeByIdSpecification(id);

            return await _scopeRepository.GetAsync(specification, cancellation) ??
                   throw new ScopeNotFoundApplicationException(id);
        }

        public async Task<IReadOnlyCollection<Scope>> GetScopesByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellation = default)
        {
            ScopeByIdsSpecification specification = new ScopeByIdsSpecification(ids);

            return await _scopeRepository.FindAsync(specification, cancellation);
        }
    }
}
