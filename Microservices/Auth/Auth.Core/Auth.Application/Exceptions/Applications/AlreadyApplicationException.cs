using OperationResults;

namespace Auth.Application.Exceptions.Applications
{
    public abstract class AlreadyApplicationException : ApplicationException
    {
        public AlreadyApplicationException(string message)
            : base(ResultErrorTypes.Already, message)
        {
        }
    }
}
