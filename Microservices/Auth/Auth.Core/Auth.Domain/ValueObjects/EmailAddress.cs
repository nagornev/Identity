using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;
using System.Text.RegularExpressions;

namespace Auth.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public const string Format = @"^[\w\.-]+@[\w\.-]+\.\w{2,}$";

        private EmailAddress(string value)
        {
            Value = value;
        }

        internal static EmailAddress Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new EmailAddressEmptyDomainException();

            if (!Regex.IsMatch(value, Format))
                throw new EmailAddressInvalidFormatDomainException();

            return new EmailAddress(value.Trim()
                                         .ToLowerInvariant());
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
