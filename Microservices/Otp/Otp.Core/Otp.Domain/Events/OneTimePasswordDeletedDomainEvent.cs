using DDD.Events;

namespace Otp.Domain.Events
{
    public class OneTimePasswordDeletedDomainEvent : DomainEvent
    {
        public OneTimePasswordDeletedDomainEvent(Guid oneTimePasswordId, Guid userId, string tag, long createdAt, bool used)
            : base(userId)
        {
            OneTimePasswordId = oneTimePasswordId;
            UserId = userId;
            Tag = tag;
            CreatedAt = createdAt;
            Used = used;
        }

        public Guid OneTimePasswordId { get; }

        public Guid UserId { get; }

        public string Tag { get; }

        public long CreatedAt { get; }

        public bool Used { get; }
    }
}
