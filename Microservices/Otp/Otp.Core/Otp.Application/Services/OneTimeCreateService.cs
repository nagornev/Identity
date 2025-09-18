using DDD.Repositories;
using Otp.Application.Abstractions.Factories;
using Otp.Application.Abstractions.Services;
using Otp.Application.DTOs;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class OneTimeCreateService : IOneTimeCreateService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IOneTimePasswordFactory _oneTimePasswordFactory;

        private readonly IRepositoryWriter<OneTimePassword> _oneTimePasswordRepository;

        private readonly IUnitOfWork _unitOfWork;

        public OneTimeCreateService(IUserQueryService userQueryService,
                                    IOneTimePasswordFactory oneTimePasswordFactory,
                                    IRepositoryWriter<OneTimePassword> oneTimePasswordRepository,
                                    IUnitOfWork unitOfWork)
        {
            _userQueryService = userQueryService;
            _oneTimePasswordFactory = oneTimePasswordFactory;
            _oneTimePasswordRepository = oneTimePasswordRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OneTimePasswordCreation> CreateAsync(Guid userId, string tag, string? payload = "", CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByIdAsync(userId);
            OneTimePassword oneTimePassword = _oneTimePasswordFactory.Create(userId, user.GetPrimaryChannel(), tag, payload);

            await _oneTimePasswordRepository.AddAsync(oneTimePassword);
            await _unitOfWork.SaveAsync(cancellation);

            return new OneTimePasswordCreation(oneTimePassword.Id,
                                               oneTimePassword.Channel.Type,
                                               oneTimePassword.Channel.Mask(),
                                               oneTimePassword.ExpiresAt);
        }
    }
}
