namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class ActionNullDomainException : NullDomainException
    {
        private const string _message = "The scope action cannot be null.";

        public ActionNullDomainException()
            : base(_message)
        {
        }
    }
}
