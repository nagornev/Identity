using OperationResults;

namespace Otp.Application.Exceptions.Infrastructures
{
    public class InvalidInfrastructureException : InfrastructureException
    {
        public InvalidInfrastructureException(string message, Exception? inner = null)
            : base(ResultErrorTypes.Invalid, message, inner)
        {
        }
    }
}
