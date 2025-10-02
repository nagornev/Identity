namespace Auth.Domain.Exceptions.Domains.Users
{
    public class TokenVersionNullDomainException : NullDomainException
    {
        private const string _message = "The token version cannot be null.";

        public TokenVersionNullDomainException()
            : base(_message)
        {
        }
    }
}
