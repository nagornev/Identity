namespace Auth.Application.Exceptions.Applications.Security
{
    public class VersionInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The session version is invalid.";

        public VersionInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
