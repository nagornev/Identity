namespace Notification.Application.Exceptions.Infrastructures
{
    public class InvalidInfrastructureException : InfrastructureException
    {
        public InvalidInfrastructureException(string message, Exception? inner = null)
            : base(InfrastructuireErrorTypes.Invalid, message, inner)
        {
        }
    }
}
