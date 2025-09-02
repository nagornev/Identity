using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using Auth.Domain.Events;
using Auth.Messaging.Abstractions.Providers;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messaging.Providers
{
    public class RolePermissionDeletedMessageContractProvider : MessageContractProvider<RolePermissionDeletedDomainEvent>
    {
        private readonly IRoleQueryService _roleQueryService;

        public RolePermissionDeletedMessageContractProvider(IRoleQueryService roleQueryService)
        {
            _roleQueryService = roleQueryService;
        }

        public override async Task<IMessageContract> Create(RolePermissionDeletedDomainEvent domainEvent)
        {
            Role role = await _roleQueryService.GetRoleByIdAsync(domainEvent.RoleId);

            return new RolePermissionDeletedMessageContract(domainEvent.AggregateId, domainEvent.RoleId, role.Name);
        }
    }
}
