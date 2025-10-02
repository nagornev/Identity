namespace Auth.Domain.Exceptions.Domains.Users
{
    public class UserAlreadyActiveDomainException : AlreadyDomainException
    {
        public UserAlreadyActiveDomainException(Guid userId)
            : base($"The user ({userId}) is already active.")
        {
        }
    }
}
