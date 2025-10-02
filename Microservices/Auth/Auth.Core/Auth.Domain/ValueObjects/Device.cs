using Auth.Domain.Exceptions.Domains.Sessions;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class Device : ValueObject
    {
        private Device(string value)
        {
            Value = value;
        }
        internal static Device Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new DeviceEmptyDomainException();

            return new Device(value);
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
