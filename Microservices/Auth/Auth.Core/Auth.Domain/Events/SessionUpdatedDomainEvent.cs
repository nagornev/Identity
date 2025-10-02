using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionUpdatedDomainEvent : DomainEvent
    {
        public SessionUpdatedDomainEvent(Guid aggregateId,
                                         Guid kid,
                                         Guid version,
                                         string device,
                                         string ipAddress)
            : base(aggregateId)
        {
            Kid = kid;
            Version = version;
            Device = device;
            IpAddress = ipAddress;
        }

        public Guid Kid { get; }

        public Guid Version { get; }

        public string Device { get; }

        public string IpAddress { get; }
    }
}
