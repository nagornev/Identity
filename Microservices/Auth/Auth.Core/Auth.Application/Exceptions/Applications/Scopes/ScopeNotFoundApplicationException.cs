namespace Auth.Application.Exceptions.Applications.Scopes
{
    public class ScopeNotFoundApplicationException : NotFoundApplicationException
    {
        public ScopeNotFoundApplicationException(Guid id)
            : base($"No scopes were found with {id} id.")
        {
        }
    }
}
