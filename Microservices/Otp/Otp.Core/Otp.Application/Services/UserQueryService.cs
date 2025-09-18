using DDD.Repositories;
using Otp.Application.Abstractions.Services;
using Otp.Application.Exceptions.Applications.Users;
using Otp.Domain.Aggregates;
using Otp.Domain.Specifications;

namespace Otp.Application.Services
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
