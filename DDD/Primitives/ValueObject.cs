namespace DDD.Primitives
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject other)
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (acc, x) => HashCode.Combine(acc, x?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            return a?.Equals(b) ?? b is null;
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
