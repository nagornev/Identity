using OperationResults;

namespace Auth.Application.Exceptions.Applications
{
    public class ApplicationException : ResultException
    {
        public ApplicationException(int type, string message)
            : base(type, message)
        {
        }

    }
}
