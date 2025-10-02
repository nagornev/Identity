namespace Notification.Domain.Exceptions.Domains.Users
{
    public class EmailAddressInvalidFormatDomainException : InvalidFormatDomainException
    {
        private const string _message = "The email address has an invalid format.";

        public EmailAddressInvalidFormatDomainException()
            : base(_message)
        {
        }
    }
}
