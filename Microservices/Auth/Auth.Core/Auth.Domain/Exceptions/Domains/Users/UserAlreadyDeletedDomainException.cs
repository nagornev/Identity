namespace Auth.Domain.Exceptions.Domains.Users
{
    public class UserAlreadyDeletedDomainException : AlreadyDomainException
    {
        public UserAlreadyDeletedDomainException(Guid userId)
            : base($"The user ({userId}) is already delete.")
        {
        }
    }
}
