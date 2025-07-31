using DDD.Primitives;
using Otp.Domain.Exceptions.Domains.Otps;

namespace Otp.Domain.ValueObjects
{
    public class Secret : ValueObject
    {
        private Secret(string value)
        {
            Value = value;
        }

        internal static Secret Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new SecretEmptyDomainException();

            return new Secret(value);
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
