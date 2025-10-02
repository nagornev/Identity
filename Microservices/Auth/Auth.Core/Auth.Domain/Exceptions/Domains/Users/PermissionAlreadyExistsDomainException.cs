namespace Auth.Domain.Exceptions.Domains.Users
{
    public class PermissionAlreadyExistsDomainException : AlreadyDomainException
    {
        private const string _message = "The permission already exists.";

        public PermissionAlreadyExistsDomainException()
            : base(_message)
        {
        }
    }
}
