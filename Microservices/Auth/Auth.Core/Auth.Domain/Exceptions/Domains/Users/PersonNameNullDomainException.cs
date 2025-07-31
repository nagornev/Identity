namespace Auth.Domain.Exceptions.Domains.Users
{
    public class PersonNameNullDomainException : NullDomainException
    {
        private const string _message = "The person name cannot be null.";

        public PersonNameNullDomainException()
            : base(_message)
        {
        }
    }
}
