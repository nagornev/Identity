namespace Auth.Application.Exceptions.Infrastructures.Persistences
{
    public class PersistenceUnavailableInfrastructureException : UnavailableInfrastructureException
    {
        private const string _message = "The persistence service is unavailable.";

        public PersistenceUnavailableInfrastructureException()
            : base(_message)
        {
        }
    }
}
