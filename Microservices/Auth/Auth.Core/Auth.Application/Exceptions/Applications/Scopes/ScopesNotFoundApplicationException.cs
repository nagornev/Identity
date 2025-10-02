namespace Auth.Application.Exceptions.Applications.Scopes
{
    public class ScopesNotFoundApplicationException : NotFoundApplicationException
    {
        public ScopesNotFoundApplicationException(string audience)
            : base($"No scopes were found from '{audience}' audience.")
        {
        }
    }
}
