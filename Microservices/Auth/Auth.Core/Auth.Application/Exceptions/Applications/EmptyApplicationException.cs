using OperationResults;

namespace Auth.Application.Exceptions.Applications
{
    public abstract class EmptyApplicationException : ApplicationException
    {
        public EmptyApplicationException(string message)
            : base(ResultErrorTypes.Empty, message)
        {
        }
    }
}
