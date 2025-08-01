namespace Auth.Application.Exceptions.Applications.Sessions
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
