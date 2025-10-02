namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class ResourceEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The scope resource cannot be empty.";

        public ResourceEmptyDomainException()
            : base(_message)
        {
        }
    }
}
