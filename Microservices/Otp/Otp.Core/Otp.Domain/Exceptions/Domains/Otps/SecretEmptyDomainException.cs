namespace Otp.Domain.Exceptions.Domains.Otps
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
