namespace Auth.Application.Exceptions.Applications.Security
{
    public class RefreshTokenInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The refresh token is invalid.";

        public RefreshTokenInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
