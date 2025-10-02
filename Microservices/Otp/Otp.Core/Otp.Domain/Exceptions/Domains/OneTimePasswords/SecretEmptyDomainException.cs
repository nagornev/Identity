namespace Otp.Domain.Exceptions.Domains.OneTimePasswords
{
    public class SecretEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The OTP secret cannot be emtpy.";

        public SecretEmptyDomainException()
            : base(_message)
        {
        }
    }
}
