using Notification.Domain.Aggregates;

namespace Notification.Application.Abstractions.Factories
{
    public interface IUserFactory
    {
        User Create(Guid userId, string email);
    }
}
