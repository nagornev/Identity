namespace Auth.Domain.Exceptions.Domains.Users
{
    public class AuthorizationNullDomainException : NullDomainException
    {
        private const string _message = "The authorization cannot be null.";

        public AuthorizationNullDomainException()
            : base(_message)
        {
        }
    }
}
