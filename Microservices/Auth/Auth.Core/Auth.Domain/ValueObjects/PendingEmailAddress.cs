using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;

namespace Auth.Domain.ValueObjects
{
    public class PendingEmailAddress : ValueObject
    {
        public PendingEmailAddress(EmailAddress emailAddress,
                                   bool isConfirmed,
                                   Guid version)
        {
            EmailAddress = emailAddress;
            IsConfirmed = isConfirmed;
            Version = version;
        }

        internal static PendingEmailAddress Create(EmailAddress emailAddress)
        {
            if (emailAddress is null)
                throw new EmailAddressNullDomainException();

            return new PendingEmailAddress(emailAddress, false, Guid.NewGuid());
        }

        internal PendingEmailAddress Confirm()
        {
            return new PendingEmailAddress(EmailAddress.Create(EmailAddress.Value), true, Version);
        }

        public EmailAddress EmailAddress { get; private set; }

        public bool IsConfirmed { get; private set; }

        public Guid Version { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
            yield return IsConfirmed;
            yield return Version;
        }

        private PendingEmailAddress()
        {
        }
    }
}
