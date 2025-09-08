namespace Otp.Application.Exceptions.Applications
{
    public abstract class NotFoundApplicationException : ApplicationException
    {
        protected NotFoundApplicationException(string message)
            : base(ApplicationErrorTypes.NotFound, message)
        {
        }
    }
}
