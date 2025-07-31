namespace Auth.Domain.Exceptions.Domains.Roles
{
    public class EntitlementNotFoundDomainException : NotFoundDomainException
    {
        private const string _message = "The entitlement with id ({0}) was not found.";

        public EntitlementNotFoundDomainException(Guid scopeId)
            : base(string.Format(_message, scopeId))
        {
        }
    }
}
