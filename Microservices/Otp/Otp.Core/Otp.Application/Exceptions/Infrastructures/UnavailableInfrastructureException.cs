using OperationResults;

namespace Otp.Application.Exceptions.Infrastructures
{
    public class UnavailableInfrastructureException : InfrastructureException
    {
        public UnavailableInfrastructureException(string message, Exception? inner = default)
            : base(ResultErrorTypes.Unavailable, message, inner)
        {
        }
    }
}
