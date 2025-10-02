using Otp.Application.Abstractions.Factories;
using Otp.Domain.Aggregates;

namespace Otp.Application.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(Guid userId, string email)
        {
            return User.Create(userId, email);
        }
    }
}
