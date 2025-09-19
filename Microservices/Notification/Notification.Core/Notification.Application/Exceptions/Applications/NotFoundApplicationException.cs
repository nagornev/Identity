using OperationResults;

namespace Notification.Application.Exceptions.Applications
{
    public abstract class NotFoundApplicationException : ApplicationException
    {
        protected NotFoundApplicationException(string message)
            : base(ResultErrorTypes.NotFound, message)
        {
        }
    }
}
