namespace Otp.Domain.Exceptions.Domains.OneTimePasswords
{
    public class OneTimePasswordAlreadyDeletedDomainException : AlreadyDomainEvent
    {
        private const string _message = "The OTP is already marked as deleted.";

        public OneTimePasswordAlreadyDeletedDomainException()
            : base(_message)
        {
        }
    }
}
