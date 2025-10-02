using Auth.Domain.Exceptions.Domains.Scopes;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class Resource : ValueObject
    {
        private Resource(string value)
        {
            Value = value;
        }

        internal static Resource Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new ResourceEmptyDomainException();

            return new Resource(value);
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
