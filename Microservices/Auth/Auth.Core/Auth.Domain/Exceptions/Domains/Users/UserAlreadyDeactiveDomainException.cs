namespace Auth.Domain.Exceptions.Domains.Users
{
    public class UserAlreadyDeactiveDomainException : AlreadyDomainException
    {
        public UserAlreadyDeactiveDomainException(Guid userId)
            : base($"The user ({userId}) is already deactive.")
        {
        }
    }
}
