namespace DDD.Primitives
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object? obj)
        {
            return obj is Entity other && EqualityComparer<Guid>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return Id!.GetHashCode();
        }
    }
}
