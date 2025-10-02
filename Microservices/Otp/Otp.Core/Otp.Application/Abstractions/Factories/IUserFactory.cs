using Otp.Domain.Aggregates;

namespace Otp.Application.Abstractions.Factories
{
    public interface IUserFactory
    {
        User Create(Guid userId, string email);
    }
}
