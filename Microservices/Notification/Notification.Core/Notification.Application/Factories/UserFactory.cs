using Notification.Application.Abstractions.Factories;
using Notification.Domain.Aggregates;

namespace Notification.Application.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(Guid userId, string email)
        {
            return User.Create(userId, email);
        }
    }
}
