namespace Auth.Application.Exceptions.Applications.Security
{
    public class PendingEmailAddressVersionInvalidApplicationException : InvalidApplicationException
    {
        public PendingEmailAddressVersionInvalidApplicationException(Guid userId)
            : base($"The pending email address version for user ({userId}) is invalid.")
        {
        }
    }
}
