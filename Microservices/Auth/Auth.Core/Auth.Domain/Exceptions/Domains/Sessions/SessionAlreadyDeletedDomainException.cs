namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class SessionAlreadyDeletedDomainException : AlreadyDomainException
    {
        private const string _message = "The session already deleted.";

        public SessionAlreadyDeletedDomainException()
            : base(_message)
        {
        }
    }
}
