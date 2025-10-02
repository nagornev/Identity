namespace Auth.Application.Exceptions.Applications.Users
{
    public class UserInvalidApplicationException : InvalidApplicationException
    {
        public UserInvalidApplicationException(Guid id)
            : base(string.Format("The user with this id ({0}) is invalid.", id))
        {
        }

        public UserInvalidApplicationException(string emailAddress)
            : base(string.Format("The user with this email address ({0}) is invalid.", emailAddress))
        {
        }
    }
}
