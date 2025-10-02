namespace Auth.Domain.Exceptions.Domains.Users
{
    public class ProfileNullDomainException : NullDomainException
    {
        private const string _message = "The profile cannot be null.";

        public ProfileNullDomainException()
            : base(_message)
        {
        }
    }
}
