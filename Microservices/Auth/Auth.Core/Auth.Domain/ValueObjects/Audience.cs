using Auth.Domain.Exceptions.Domains.Scopes;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class Audience : ValueObject
    {
        private Audience(string value)
        {

            Value = value;
        }

        internal static Audience Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new AudienceEmptyDomainException();

            return new Audience(value);
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
