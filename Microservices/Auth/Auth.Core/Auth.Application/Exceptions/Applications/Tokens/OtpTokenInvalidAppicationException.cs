namespace Auth.Application.Exceptions.Applications.Tokens
{
    public class OtpTokenInvalidAppicationException : InvalidApplicationException
    {
        private const string _message = "The OTP token is invalid.";

        public OtpTokenInvalidAppicationException()
            : base(_message)
        {
        }
    }
}
