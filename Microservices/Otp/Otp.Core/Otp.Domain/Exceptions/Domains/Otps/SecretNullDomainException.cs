namespace Otp.Domain.Exceptions.Domains.Otps
{
    public class SecretNullDomainException : NullDomainException
    {
        private const string _message = "The OTP secret cannot be null.";

        public SecretNullDomainException()
            : base(_message)
        {
        }
    }
}
