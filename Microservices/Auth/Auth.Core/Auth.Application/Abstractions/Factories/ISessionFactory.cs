using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Factories
{
    public interface ISessionFactory
    {
        Session Create(Guid userId, Guid kid, string publicKey, string audience, string device, string ipAddress);
    }
}
