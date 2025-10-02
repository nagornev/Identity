namespace Auth.Domain.Exceptions.Domains.Scopes
{
    public class ActionEmptyDomainException : EmptyDomainException
    {
        private const string _message = "The scope action cannot be empty.";

        public ActionEmptyDomainException()
            : base(_message)
        {
        }
    }
}
