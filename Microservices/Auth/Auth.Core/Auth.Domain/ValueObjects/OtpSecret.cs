using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class OtpSecret : ValueObject
    {
        private OtpSecret(string value)
        {
            Value = value;
        }

        internal static OtpSecret Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new TFASecretEmptyDomainException();

            return new OtpSecret(value);
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
