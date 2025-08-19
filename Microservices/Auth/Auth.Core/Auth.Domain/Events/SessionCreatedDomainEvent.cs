using DDD.Events;

namespace Auth.Domain.Events
{
    public class SessionCreatedDomainEvent : DomainEvent
    {
        public SessionCreatedDomainEvent(Guid aggregateId,
                                         Guid kid,
                                         Guid version,
                                         string publicKey)
            : base(aggregateId)
        {
            Kid = kid;
            Version = version;
            PublicKey = publicKey;
        }

        public Guid Kid { get; }

        public Guid Version { get; }

        public string PublicKey { get; }
    }
}
