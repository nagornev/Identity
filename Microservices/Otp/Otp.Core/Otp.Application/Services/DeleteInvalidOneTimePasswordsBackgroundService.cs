using DDD.Repositories;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class DeleteInvalidOneTimePasswordsBackgroundService : IDeleteInvalidOneTimePasswordsBackgroundService
    {
        private readonly IOneTimePasswordQueryService _oneTimePasswordQueryService;

        private readonly IRepositoryWriter<OneTimePassword> _oneTimePasswordRepository;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteInvalidOneTimePasswordsBackgroundService(IOneTimePasswordQueryService oneTimePasswordQueryService,
                                                              IRepositoryWriter<OneTimePassword> oneTimePasswordRepository,
                                                              ITimeProvider timeProvider,
                                                              IUnitOfWork unitOfWork)
        {
            _oneTimePasswordQueryService = oneTimePasswordQueryService;
            _oneTimePasswordRepository = oneTimePasswordRepository;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(CancellationToken cancellation = default)
        {
            IReadOnlyCollection<OneTimePassword> invalidOneTimePasswords = await _oneTimePasswordQueryService.GetExpiredOneTimePasswordsAsync(_timeProvider.NowUnix());

            foreach (OneTimePassword invalidOneTimePassword in invalidOneTimePasswords)
            {
                invalidOneTimePassword.MarkAsDeleted();
                await _oneTimePasswordRepository.DeleteAsync(invalidOneTimePassword);
            }

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
