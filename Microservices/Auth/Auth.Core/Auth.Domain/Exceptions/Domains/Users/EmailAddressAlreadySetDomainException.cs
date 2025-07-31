namespace Auth.Domain.Exceptions.Domains.Users
{
    public class EmailAddressAlreadySetDomainException : AlreadyDomainException
    {
        public EmailAddressAlreadySetDomainException(string emailAddress)
            : base($"The email ({emailAddress}) is already set for this user.")
        {
        }
    }
}
