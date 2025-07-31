namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class TFATokenInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "This TFA token is invalid.";

        public TFATokenInvalidApplicationException(string tfaToken) : base(_message)
        {
        }
    }
}
