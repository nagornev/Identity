namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class TFACodeInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "This TFA code is invalid.";

        public TFACodeInvalidApplicationException(Guid userId, string tfaCode)
            : base(_message)
        {
        }
    }
}
