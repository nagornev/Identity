namespace Auth.Application.Exceptions.Applications.Sessions
{
    internal class SessionInvalidApplicationException : InvalidApplicationException
    {
        public SessionInvalidApplicationException(Guid sessionId)
            : base($"The session ({sessionId}) is invalid.")
        {
        }
    }
}
