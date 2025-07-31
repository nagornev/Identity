using Auth.Domain.Aggregates;

namespace Auth.Application.Abstractions.Factories
{
    public interface IUserFactory
    {
        User Create(string email, string personName, string password);
    }
}
