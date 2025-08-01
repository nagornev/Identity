using DDD.Primitives;
using Otp.Domain.Entities;
using Otp.Domain.Exceptions.Domains.Otps;
using Otp.Domain.ValueObjects;

namespace Otp.Domain.Aggregates
{
    public class Otp : AggregateRoot
    {
        private Otp(Guid id, Secret secret, Delivery delivery)
        {
            Id = id;
            Secret = secret;
            Delivery = delivery;
        }

        public static Otp Create(Guid id, string secret, string emailAddress)
        {
            Otp otp = new Otp(id,
                              Secret.Create(secret) ??
                              throw new SecretNullDomainException(),

                              Delivery.Create(id,
                                              EmailAddress.Create(emailAddress) ??
                                              throw new EmailAddressNullDomainException()) ??
                              throw new DeliveryNullDomainException());

            return otp;
        }


        public Secret Secret { get; private set; }

        public Delivery Delivery { get; private set; }

        public void ChangeEmailAddress(string email)
        {
            EmailAddress emailAddress = EmailAddress.Create(email) ??
                                        throw new EmailAddressNullDomainException();

            Delivery.ChangeEmailAddress(emailAddress);
        }
    }
}
