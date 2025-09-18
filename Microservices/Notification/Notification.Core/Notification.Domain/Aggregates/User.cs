using DDD.Primitives;
using Notification.Domain.Exceptions.Domains.Users;
using Notification.Domain.ValueObjects;

namespace Notification.Domain.Aggregates
{
    public partial class User : AggregateRoot
    {
        private User(Guid id,
                     EmailAddress emailAddress)
        {
            Id = id;
            EmailAddress = emailAddress;
        }

        public EmailAddress EmailAddress { get; private set; }

        public static User Create(Guid id, string email)
        {
            return new User(id,

                            EmailAddress.Create(email) ??
                            throw new EmailAddressNullDomainException()); ;
        }

        public void ChangeEmailAddress(string email)
        {
            EmailAddress = EmailAddress.Create(email) ??
                           throw new EmailAddressNullDomainException();
        }
    }

    public partial class User
    {
        private User()
        {
            
        }
    }
}
