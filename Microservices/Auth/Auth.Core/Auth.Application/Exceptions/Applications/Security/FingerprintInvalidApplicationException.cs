namespace Auth.Application.Exceptions.Applications.Security
{
    public class FingerprintInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The fingerprint is invalid.";

        public FingerprintInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
