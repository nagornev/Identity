using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class PendingPasswordHash : ValueObject
    {
        private PendingPasswordHash(PasswordHash passwordHash, Guid version)
        {
            PasswordHash = passwordHash;
            Version = version;
        }

        internal static PendingPasswordHash Create(PasswordHash passwordHash)
        {
            if (passwordHash is null)
                throw new PasswordHashNullDomainException();

            return new PendingPasswordHash(passwordHash, Guid.NewGuid());
        }

        public PasswordHash PasswordHash { get; private set; }

        public Guid Version { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PasswordHash;
            yield return Version;
        }

        private PendingPasswordHash()
        {
        }
    }
}
