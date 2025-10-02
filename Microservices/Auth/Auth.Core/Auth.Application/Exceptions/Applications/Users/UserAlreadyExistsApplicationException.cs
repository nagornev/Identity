namespace Auth.Application.Exceptions.Applications.Users
{
    public class UserAlreadyExistsApplicationException : AlreadyApplicationException
    {
        public UserAlreadyExistsApplicationException(Guid id)
            : base(string.Format("The user with this ID ({0}) already exists.", id))
        {
        }

        public UserAlreadyExistsApplicationException(string email)
            : base(string.Format("The user with this email ({0}) already exists.", email))
        {
        }
    }
}
