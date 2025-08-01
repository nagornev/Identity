using DDD.Events;

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
