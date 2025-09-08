namespace Otp.Application.Exceptions.Infrastructures
{
    public class CanceledInfrastructureException : InfrastructureException
    {
        public CanceledInfrastructureException(string message, Exception? inner = null)
            : base(InfrastructuireErrorTypes.Canceled, message, inner)
        {
        }
    }
}
