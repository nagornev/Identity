namespace Auth.Domain.Exceptions.Domains.Users
{
    public class TokenVersionEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The token version cannot be empty.";

        public TokenVersionEmptyDomainException()
            : base(_message)
        {
        }
    }
}
