namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class ResourceNullDomainException : NullDomainException
    {
        private const string _message = "The scope resource cannot be null.";

        public ResourceNullDomainException()
            : base(_message)
        {
        }
    }
}
