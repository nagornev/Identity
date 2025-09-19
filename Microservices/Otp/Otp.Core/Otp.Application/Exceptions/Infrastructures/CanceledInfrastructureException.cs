using OperationResults;

namespace Otp.Application.Exceptions.Infrastructures
{
    public class CanceledInfrastructureException : InfrastructureException
    {
        public CanceledInfrastructureException(string message, Exception? inner = null)
            : base(ResultErrorTypes.Canceled, message, inner)
        {
        }
    }
}
