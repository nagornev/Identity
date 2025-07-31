namespace Auth.Domain.Exceptions.Domains.Users
{
    public class TFASecretNullDomainException : NullDomainException
    {
        private const string _message = "The TFA secret cannot be empty.";

        public TFASecretNullDomainException()
            : base(_message)
        {
        }
    }
}
