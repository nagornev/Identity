namespace Auth.Domain.Exceptions.Domains.Users
{
    public class TFASecretEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The TFA secret cannot be empty.";

        public TFASecretEmptyDomainException()
            : base(_message)
        {
        }
    }
}
