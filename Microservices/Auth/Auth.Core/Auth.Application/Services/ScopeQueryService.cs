using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;
using DDD.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class ScopeQueryService : IScopeQueryService
    {
        private readonly IRepositoryReader<Scope> _scopeRepository;

        public ScopeQueryService(IRepositoryReader<Scope> scopeRepository)
        {
            _scopeRepository = scopeRepository;
        }

        public async Task<IReadOnlyCollection<Scope>> GetScopesByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellation = default)
        {
            ScopeByIdsSpecification specification = new ScopeByIdsSpecification(ids);

            return await _scopeRepository.FindAsync(specification, cancellation);
        }
    }
}
