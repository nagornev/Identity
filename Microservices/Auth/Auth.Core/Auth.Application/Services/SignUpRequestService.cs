using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class SignUpRequestService : ISignUpRequestService
    {
        private readonly IUserCreateService _userCreateService;

        private readonly IRepositoryWriter<User> _userRepository;

        private readonly IUnitOfWork _unitOfWork;

        public SignUpRequestService(IUserCreateService userCreateService,
                                    IRepositoryWriter<User> userRepository,
                                    IUnitOfWork unitOfWork)
        {
            _userCreateService = userCreateService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task RequestAsync(string emailAddress, string personName, string password, CancellationToken cancellation = default)
        {
            User user = await _userCreateService.CreateAsync(emailAddress, personName, password, cancellation);
            await _userRepository.AddAsync(user, cancellation);

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
