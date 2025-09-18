using DDD.Repositories;
using Notification.Application.Abstractions.Services;
using Notification.Application.Exceptions.Applications.Users;
using Notification.Domain.Aggregates;
using Notification.Domain.Specifications;

namespace Notification.Application.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IRepositoryReader<User> _userRepository;

        public UserQueryService(IRepositoryReader<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellation = default)
        {
            UserByIdSpecification specification = new UserByIdSpecification(userId);

            return await _userRepository.GetAsync(specification, cancellation) ??
                   throw new UserNotFoundApplicationException(userId);
        }
    }
}
