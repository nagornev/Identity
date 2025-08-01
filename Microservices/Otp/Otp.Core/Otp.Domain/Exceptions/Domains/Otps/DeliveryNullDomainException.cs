namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class DeliveryNullDomainException : NullDomainException
    {
        private const string _message = "The OTP delivery cannot be null.";

        public DeliveryNullDomainException()
            : base(_message)
        {
        }
    }
}
