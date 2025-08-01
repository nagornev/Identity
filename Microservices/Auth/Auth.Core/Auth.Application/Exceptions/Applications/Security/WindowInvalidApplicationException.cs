namespace Auth.Application.Exceptions.Applications.Security
{
    public class WindowInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The request window is invalid.";

        public WindowInvalidApplicationException()
            : base(_message)
        {
        }
    }
}
