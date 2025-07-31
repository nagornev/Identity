using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Events
{
    public class RolePermissionDeletedDomainEvent : DomainEvent
    {
        public RolePermissionDeletedDomainEvent(Guid aggregateId, Guid roleId) 
            : base(aggregateId)
        {
            RoleId = roleId;
        }

        public Guid RoleId { get; }
    }
}
