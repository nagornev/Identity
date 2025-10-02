using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        private PasswordHash(string value)
        {
            Value = value;
        }

        internal static PasswordHash Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new PasswordHashEmptyDomainException();

            return new PasswordHash(value);
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
