namespace Otp.Application.Exceptions.Applications.Users
{
    public class UserNotFoundApplicationException : NotFoundApplicationException
    {
        public UserNotFoundApplicationException(Guid id)
           : base(string.Format("The user with this id ({0}) was not found.", id))
        {
        }
    }
}
