using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class ScopePermissionDeletedMessageContractProvider : MessageContractProvider<ScopePermissionDeletedDomainEvent>
    {
        private readonly IScopeQueryService _scopeQueryService;

        public ScopePermissionDeletedMessageContractProvider(IScopeQueryService scopeQueryService)
        {
            _scopeQueryService = scopeQueryService;
        }

        public override async Task<dynamic> Create(ScopePermissionDeletedDomainEvent domainEvent)
        {
            Scope scope = await _scopeQueryService.GetScopeByIdAsync(domainEvent.ScopeId);

            return new ScopePermissionDeletedMessageContract(domainEvent.AggregateId, domainEvent.ScopeId, scope.Audience.Value, scope.GetName());
        }
    }
}
