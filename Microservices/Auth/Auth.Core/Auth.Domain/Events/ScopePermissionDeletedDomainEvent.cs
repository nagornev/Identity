using DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Events
{
    public class ScopePermissionDeletedDomainEvent : DomainEvent
    {
        public ScopePermissionDeletedDomainEvent(Guid aggregateId,
                                                 Guid scopeId)
            : base(aggregateId)
        {
            ScopeId = scopeId;
        }

        public Guid ScopeId { get; }
    }
}
