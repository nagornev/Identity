namespace Auth.Application.Exceptions.Applications
{
    public abstract class NotFoundApplicationException : ApplicationException
    {
        public NotFoundApplicationException(string message)
            : base(ApplicationErrorTypes.NotFound, message)
        {
        }
    }
}
