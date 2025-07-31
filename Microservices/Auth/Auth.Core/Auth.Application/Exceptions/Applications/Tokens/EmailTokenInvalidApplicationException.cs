namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class EmailTokenInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The email token is invalid.";

        public EmailTokenInvalidApplicationException(string token)
            : base(_message)
        {
        }
    }
}
