namespace Auth.Application.Exceptions.Applications.Security
{
    public class ChannelTokenInvalidApplicationException : InvalidApplicationException
    {
        private const string _message = "The channel token is invalid.";

        public ChannelTokenInvalidApplicationException(string token)
            : base(_message)
        {
        }
    }
}
