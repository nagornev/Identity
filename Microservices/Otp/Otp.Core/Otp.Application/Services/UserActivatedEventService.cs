using DDD.Repositories;
using Otp.Application.Abstractions.Factories;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class UserActivatedEventService : IUserActivatedEventService
    {
        private readonly IUserFactory _userFactory;

        private readonly IRepositoryWriter<User> _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public UserActivatedEventService(IUserFactory userFactory,
                                         IRepositoryWriter<User> userRepository,
                                         IUnitOfWork unitOfWork)
        {
            _userFactory = userFactory;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(Guid userId, string email, CancellationToken cancellation = default)
        {
            User user = _userFactory.Create(userId, email);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
