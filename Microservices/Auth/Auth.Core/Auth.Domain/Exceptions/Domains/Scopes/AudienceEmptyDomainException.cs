namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class AudienceEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The audience can not be empty.";

        public AudienceEmptyDomainException()
            : base(_message)
        {
        }
    }
}
