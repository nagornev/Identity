using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using Auth.Domain.Events;
using MessageContracts;

namespace Auth.Messaging.Providers
{
    public class RolePermissionDeletedMessageContractProvider : MessageContractProvider<RolePermissionDeletedDomainEvent>
    {
        private readonly IRoleQueryService _roleQueryService;

        public RolePermissionDeletedMessageContractProvider(IRoleQueryService roleQueryService)
        {
            _roleQueryService = roleQueryService;
        }

        public override async Task<dynamic> Create(RolePermissionDeletedDomainEvent domainEvent)
        {
            Role role = await _roleQueryService.GetRoleByIdAsync(domainEvent.RoleId);

            return new RolePermissionDeletedMessageContract(domainEvent.AggregateId, domainEvent.RoleId, role.Name);
        }
    }
}
