namespace Auth.Domain.Exceptions.Domains.Roles
{
    public class EntitlementAreadyExistsDomainException : AlreadyDomainException
    {
        private const string _message = "The entitlement with id ({0}) already exists.";

        public EntitlementAreadyExistsDomainException(Guid scopeId)
            : base(string.Format(_message, scopeId))
        {
        }
    }
}
