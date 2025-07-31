namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class EmailAddressNullDomainException : NullDomainException
    {
        private const string _message = "The email address cannot be null.";

        public EmailAddressNullDomainException()
            : base(_message)
        {
        }
    }
}
