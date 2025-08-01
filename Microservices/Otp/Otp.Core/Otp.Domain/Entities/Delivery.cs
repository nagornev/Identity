using DDD.Primitives;
using Otp.Domain.ValueObjects;

namespace Otp.Domain.Entities
{
    public class Delivery : Entity
    {
        private Delivery(Guid id,
                         EmailAddress emailAddress)
        {
            Id = id;
            EmailAddress = emailAddress;
        }

        internal static Delivery Create(Guid id, EmailAddress emailAddress)
        {
            return new Delivery(id,
                                emailAddress);
        }

        public EmailAddress EmailAddress { get; private set; }

        internal void ChangeEmailAddress(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
