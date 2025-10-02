using DDD.Primitives;
using Otp.Domain.Exceptions.Domains.OneTimePasswords;

namespace Otp.Domain.ValueObjects
{
    public class Secret : ValueObject
    {
        private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

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

        public byte[] DecodeToBytes()
        {
            return Convert.FromBase64String(Value);
        }
    }
}
