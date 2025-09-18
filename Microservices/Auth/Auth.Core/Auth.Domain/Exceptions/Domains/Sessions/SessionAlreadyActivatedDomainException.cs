namespace Auth.Domain.Exceptions.Domains.Sessions
{
    public class SessionAlreadyActivatedDomainException : AlreadyDomainException
    {
        private const string _message = "The session already activated.";

        public SessionAlreadyActivatedDomainException()
            : base(_message)
        {
        }
    }
}
