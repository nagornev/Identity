using OperationResults;

namespace Auth.Application.Exceptions.Applications
{
    public abstract class NullApplicationException : ApplicationException
    {
        public NullApplicationException(string message) 
            : base(ResultErrorTypes.Null, message)
        {
        }
    }
}
