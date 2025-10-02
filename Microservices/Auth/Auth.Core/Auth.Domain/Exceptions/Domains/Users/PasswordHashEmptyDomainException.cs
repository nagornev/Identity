namespace Auth.Domain.Exceptions.Domains.Users
{
    public class PasswordHashEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The password hash cannot be empty.";

        public PasswordHashEmptyDomainException()
            : base(_message)
        {
        }
    }
}
