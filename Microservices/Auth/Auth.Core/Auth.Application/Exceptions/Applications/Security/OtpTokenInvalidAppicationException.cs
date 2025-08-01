namespace Auth.Application.Exceptions.Applications.Security
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
