namespace Auth.Application.Exceptions.Applications.Sessions
{
    public class SessionNotFoundApplicationException : NotFoundApplicationException
    {
        public SessionNotFoundApplicationException(Guid id)
            : base(string.Format($"The session with this ID ({id}) was not found."))
        {
        }

        public SessionNotFoundApplicationException(string name)
            : base(string.Format($"The session with this name ({name}) was not found."))
        {
        }
    }
}
