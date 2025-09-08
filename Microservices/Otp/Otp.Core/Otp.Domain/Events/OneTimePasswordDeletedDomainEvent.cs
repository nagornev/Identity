using DDD.Events;

namespace Otp.Domain.Events
{
    public class OneTimePasswordDeletedDomainEvent : DomainEvent
    {
        public OneTimePasswordDeletedDomainEvent(Guid oneTimePasswordId, Guid subject, string tag, long createdAt, bool used)
            : base(subject)
        {
            OneTimePasswordId = oneTimePasswordId;
            Subject = subject;
            Tag = tag;
            CreatedAt = createdAt;
            Used = used;
        }

        public Guid OneTimePasswordId { get; }

        public Guid Subject { get; }

        public string Tag { get; }

        public long CreatedAt { get; }

        public bool Used { get; }
    }
}
