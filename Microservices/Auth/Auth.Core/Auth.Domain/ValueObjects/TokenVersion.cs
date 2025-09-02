using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class TokenVersion : ValueObject
    {
        private TokenVersion(Guid value)
        {
            Value = value;
        }

        internal static TokenVersion Create(Guid value)
        {
            if (value == Guid.Empty)
                throw new TokenVersionEmptyDomainException();

            return new TokenVersion(value);
        }

        public Guid Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
