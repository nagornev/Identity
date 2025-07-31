namespace Auth.Application.Exceptions.Applications
{
    public abstract class InvalidApplicationException : ApplicationException
    {
        public InvalidApplicationException(string message)
            : base(ApplicationErrorTypes.Invalid, message)
        {
        }
    }
}
