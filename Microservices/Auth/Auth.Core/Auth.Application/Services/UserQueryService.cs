using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Users;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;
using System.Threading.Tasks;

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
            UserIdSpecification specification = new UserIdSpecification(id);

            return await _userRepository.GetAsync(specification, cancellation) ??
                   throw new UserNotFoundApplicationException(id);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellation = default)
        {
            UserEmailSpecification specification = new UserEmailSpecification(email);

            return await _userRepository.GetAsync(specification, cancellation) ??
                   throw new UserNotFoundApplicationException(email);
        }

        public async Task<bool> IsUserAlreadyExistsAsync(string email, CancellationToken cancellation = default)
        {
            UserEmailSpecification emailSpecification = new UserEmailSpecification(email);
            UserActiveSpecification activeSpecification = new UserActiveSpecification(true);

            return await _userRepository.ExistsAsync(emailSpecification.And(activeSpecification), cancellation);
        }

        public IAsyncEnumerable<User> FindUnactivatedUsersAsyncStream(long timestamp)
        {
            UserActiveSpecification userActiveSpecification = new UserActiveSpecification(false);
            UserCreatedBeforeSpecification userCreatedBeforeSpecification = new UserCreatedBeforeSpecification(timestamp);

            return _userRepository.AsyncStream(userActiveSpecification.And(userCreatedBeforeSpecification));
        }

        public IAsyncEnumerable<User> FindUsersWithInvalidPermissionsAsyncStream(long timestamp)
        {
            UserInvalidPermissionsSpecification specification = new UserInvalidPermissionsSpecification(timestamp);

            return _userRepository.AsyncStream(specification);
        }
    }
}
