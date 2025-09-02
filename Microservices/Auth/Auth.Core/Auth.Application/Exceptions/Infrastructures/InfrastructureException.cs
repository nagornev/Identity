using OperationResults;

namespace Auth.Application.Exceptions.Infrastructures
{
    public class InfrastructureException : ResultException
    {
        public InfrastructureException(int type, string message, Exception? inner = default)
            : base(type, message, inner)
        {
        }
    }
}
