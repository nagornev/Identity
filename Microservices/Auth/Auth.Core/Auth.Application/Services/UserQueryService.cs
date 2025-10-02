using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IRepositoryReader<User> _userRepository;

        public UserQueryService(IRepositoryReader<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            UserByIdSpecification specification = new UserByIdSpecification(id);

            return await _userRepository.GetAsync(specification, cancellation) ??
                   throw new UserNotFoundApplicationException(id);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellation = default)
        {
            UserByEmailSpecification specification = new UserByEmailSpecification(email);

            return await _userRepository.GetAsync(specification, cancellation) ??
                   throw new UserNotFoundApplicationException(email);
        }

        public async Task<bool> IsUserAlreadyExistsAsync(string email, CancellationToken cancellation = default)
        {
            UserByEmailSpecification emailSpecification = new UserByEmailSpecification(email);
            UserByActiveSpecification activeSpecification = new UserByActiveSpecification(true);

            return await _userRepository.ExistsAsync(emailSpecification.And(activeSpecification), cancellation);
        }

        public IAsyncEnumerable<User> FindUnactivatedUsersAsyncStream(long timestamp)
        {
            UserByActiveSpecification userActiveSpecification = new UserByActiveSpecification(false);
            UserByCreatedBeforeSpecification userCreatedBeforeSpecification = new UserByCreatedBeforeSpecification(timestamp);

            return _userRepository.AsyncStream(userActiveSpecification.And(userCreatedBeforeSpecification));
        }

        public IAsyncEnumerable<User> FindUsersWithInvalidPermissionsAsyncStream(long timestamp)
        {
            UserByInvalidPermissionSpecification specification = new UserByInvalidPermissionSpecification(timestamp);

            return _userRepository.AsyncStream(specification);
        }
    }
}
