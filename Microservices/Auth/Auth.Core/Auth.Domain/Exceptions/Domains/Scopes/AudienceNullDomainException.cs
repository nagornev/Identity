namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class AudienceNullDomainException : NullDomainException
    {
        private const string _message = "The audience can not be null.";

        public AudienceNullDomainException()
            : base(_message)
        {
        }
    }
}
