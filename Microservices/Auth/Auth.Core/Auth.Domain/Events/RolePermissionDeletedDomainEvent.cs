using DDD.Events;

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
