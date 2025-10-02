namespace Auth.Domain.Exceptions.Domains.Users
{
    public class PasswordHashAlreadySetDomainException : AlreadyDomainException
    {
        private const string _message = "This password has already been set.";

        public PasswordHashAlreadySetDomainException()
            : base(_message)
        {
        }
    }
}
