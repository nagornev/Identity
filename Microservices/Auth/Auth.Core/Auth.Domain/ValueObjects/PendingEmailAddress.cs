using Auth.Domain.Exceptions.Domains.Users;
using DDD.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (emailAddress == null)
                throw new EmailAddressNullDomainException();

            return new PendingEmailAddress(emailAddress, false, Guid.NewGuid());
        }

        internal PendingEmailAddress Confirm()
        {
            return new PendingEmailAddress(EmailAddress, true, Version);
        }

        public EmailAddress EmailAddress { get; }

        public bool IsConfirmed { get; }

        public Guid Version { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
            yield return IsConfirmed;
            yield return Version;
        }
    }
}
