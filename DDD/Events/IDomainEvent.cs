namespace DDD.Events
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }

        long OccurredOn { get; }
    }
}
