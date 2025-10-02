using Auth.Domain.Exceptions.Domains.Scopes;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class Action : ValueObject
    {
        private Action(string value)
        {
            Value = value;
        }

        internal static Action Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
                throw new ActionEmptyDomainException();

            return new Action(value);
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
