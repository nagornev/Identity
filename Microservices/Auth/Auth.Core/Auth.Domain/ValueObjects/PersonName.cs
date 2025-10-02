using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class PersonName : ValueObject
    {
        public const int MinimumLength = 2;

        public const int MaximumLength = 30;

        private PersonName(string name)
        {
            Name = name;
        }

        internal static PersonName Create(string name)
        {
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrWhiteSpace(name))
                throw new PersonNameEmptyDomainException();

            if (name.Length < MinimumLength ||
                name.Length > MaximumLength)
                throw new PersonNameLengthOutOfRangeDomainException(MinimumLength, MaximumLength);

            return new PersonName(name);
        }

        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
