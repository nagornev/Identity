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
            return Base32Decode(Value);
        }

        private static byte[] Base32Decode(string base32)
        {
            int bitBuffer = 0, bitCount = 0;
            var result = new List<byte>();

            foreach (char c in base32.TrimEnd('=').ToUpperInvariant())
            {
                int val = _alphabet.IndexOf(c);
                if (val < 0) throw new ArgumentException("Invalid Base32 char");

                bitBuffer = (bitBuffer << 5) | val;
                bitCount += 5;

                if (bitCount >= 8)
                {
                    result.Add((byte)(bitBuffer >> (bitCount - 8)));
                    bitCount -= 8;
                }
            }

            return result.ToArray();
        }
    }
}
