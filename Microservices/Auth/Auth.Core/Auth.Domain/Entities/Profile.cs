using Auth.Domain.ValueObjects;
using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class Profile : Entity
    {
        private Profile(Guid id,
                        EmailAddress emailAddress,
                        PersonName personName)
        {
            Id = id;
            EmailAddress = emailAddress;
            PersonName = personName;
        }
        internal static Profile Create(Guid id,
                                       EmailAddress emailAddress,
                                       PersonName personName)
        {
            return new Profile(id,
                               emailAddress,
                               personName);
        }

        public PendingEmailAddress? PendingEmailAddress { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public PersonName PersonName { get; private set; }

        internal void ChangeEmailAddress(PendingEmailAddress pendingEmailAddress)
        {
            PendingEmailAddress = pendingEmailAddress;
        }

        internal void ConfirmEmailAddressChange()
        {
            PendingEmailAddress = PendingEmailAddress!.Confirm();
        }

        internal void UpdateEmailAddress()
        {
            EmailAddress = PendingEmailAddress!.EmailAddress;
            PendingEmailAddress = null;
        }

        internal void ChangePersonName(PersonName personName)
        {
            PersonName = personName;
        }
    }

    #region Ef

    public partial class Profile
    {
        private Profile()
        {
        }
    }

    #endregion
}
