namespace DDD.Events
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            OccurredOn = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public Guid AggregateId { get; }

        public long OccurredOn { get; }
    }
}
