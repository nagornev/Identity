namespace Notification.Application.Exceptions.Infrastructures.Persistences
{
    public class PersistenceInvalidOperationInfrastructureException : InvalidInfrastructureException
    {
        private const string _message = "The invalid operation in the persistence.";

        public PersistenceInvalidOperationInfrastructureException(Exception? inner = null)
            : base(_message, inner)
        {
        }
    }
}
