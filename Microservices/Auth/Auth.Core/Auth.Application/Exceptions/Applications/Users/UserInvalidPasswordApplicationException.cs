namespace Auth.Application.Exceptions.Applications.Users
{
    public class UserInvalidPasswordApplicationException : InvalidApplicationException
    {
        private const string _message = "Invalid password.";

        public UserInvalidPasswordApplicationException(Guid userId)
            : base(_message)
        {
        }

        public UserInvalidPasswordApplicationException(string emailAddress)
            : base(_message)
        {
        }
    }
}
