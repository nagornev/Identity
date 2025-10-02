namespace Otp.Application.Exceptions.Infrastructures.Persistences
{
    public class PersistenceOperationCanceledInfrastructureException : CanceledInfrastructureException
    {
        private const string _message = "The persistence operation was cancel.";

        public PersistenceOperationCanceledInfrastructureException(Exception? inner = null)
            : base(_message, inner)
        {
        }
    }
}
