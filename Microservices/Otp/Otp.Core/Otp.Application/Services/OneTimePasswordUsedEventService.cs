using DDD.Repositories;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class OneTimePasswordUsedEventService : IOneTimePasswordUsedEventService
    {
        private readonly IOneTimePasswordQueryService _oneTimePasswordQueryService;

        private readonly IRepositoryWriter<OneTimePassword> _oneTimePasswordRepository;

        private readonly IUnitOfWork _unitOfWork;

        public OneTimePasswordUsedEventService(IOneTimePasswordQueryService oneTimePasswordQueryService,
                                               IRepositoryWriter<OneTimePassword> oneTimePasswordRepository,
                                               IUnitOfWork unitOfWork)
        {
            _oneTimePasswordQueryService = oneTimePasswordQueryService;
            _oneTimePasswordRepository = oneTimePasswordRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(Guid oneTimePasswordId, CancellationToken cancellation = default)
        {
            OneTimePassword oneTimePassword = await _oneTimePasswordQueryService.GetOneTimePasswordByIdAsync(oneTimePasswordId, cancellation);
            oneTimePassword.MarkAsDeleted();

            await _oneTimePasswordRepository.DeleteAsync(oneTimePassword, cancellation);
            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
