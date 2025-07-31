namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class EmailAddressEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The email address cannot be empty.";

        public EmailAddressEmptyDomainException()
            : base(_message)
        {
        }
    }
}
