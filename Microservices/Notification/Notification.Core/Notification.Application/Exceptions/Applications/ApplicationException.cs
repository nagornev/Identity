using OperationResults;

namespace Notification.Application.Exceptions.Applications
{
    public abstract class ApplicationException : ResultException
    {
        public ApplicationException(int type, string message)
            : base(type, message)
        {
        }
    }
}
