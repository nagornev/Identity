namespace Auth.Application.Exceptions.Applications.Security
{
    public class OtpInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The OTP is invalid.";

        public OtpInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
