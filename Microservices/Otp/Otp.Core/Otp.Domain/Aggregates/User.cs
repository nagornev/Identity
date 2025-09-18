using DDD.Primitives;
using Otp.Domain.Exceptions.Domains.Users;
using Otp.Domain.ValueObjects;

namespace Otp.Domain.Aggregates
{
    public partial class User : AggregateRoot
    {
        public User(Guid id,
                    EmailAddress emailAddress)
        {
            Id = id;
            EmailAddress = emailAddress;
        }

        public static User Create(Guid userId, string email)
        {
            return new User(userId,

                            EmailAddress.Create(email) ??
                            throw new EmailAddressNullDomainException());
        }

        public EmailAddress EmailAddress { get; private set; }

        public Channel GetPrimaryChannel()
        {
            return Channel.CreateEmailAddressChannel(EmailAddress.Value);
        }

        public void ChangeEmailAddress(string email)
        {
            EmailAddress = EmailAddress.Create(email) ??
                           throw new EmailAddressNullDomainException();
        }
    }
    #region Ef
    public partial class User
    {
        private User()
        {

        }
    }
    #endregion
}
