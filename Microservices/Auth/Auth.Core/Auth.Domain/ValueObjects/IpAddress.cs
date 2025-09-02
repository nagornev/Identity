using Auth.Domain.Exceptions.Domains.Sessions;
using DDD.Primitives;
using System.Text.RegularExpressions;

namespace Auth.Domain.ValueObjects
{
    public class IpAddress : ValueObject
    {
        public const string Format = @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$";

        private IpAddress(string value)
        {
            Value = value;
        }

        internal static IpAddress Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new IpAddressEmptyDomainException();

            if (!Regex.IsMatch(value, Format))
                throw new IpAddressInvalidFormatDomainException();

            return new IpAddress(value);
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
