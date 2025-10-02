namespace Auth.Application.Exceptions.Applications.Users
{
    public class UserNotFoundApplicationException : NotFoundApplicationException
    {
        public UserNotFoundApplicationException(Guid id)
            : base(string.Format("The user with this id ({0}) was not found.", id))
        {
        }

        public UserNotFoundApplicationException(string email)
            : base(string.Format("The user with this email ({0}) was not found.", email))
        {
        }
    }
}
