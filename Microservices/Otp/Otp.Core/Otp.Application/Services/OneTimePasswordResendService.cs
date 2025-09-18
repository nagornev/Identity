using DDD.Repositories;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class OneTimePasswordResendService : IOneTimePasswordResendService
    {
        private readonly IOneTimePasswordQueryService _oneTimePasswordQueryService;

        private readonly ISecretProvider _secretProvider;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public OneTimePasswordResendService(IOneTimePasswordQueryService oneTimePasswordQueryService,
                                            ISecretProvider secretProvider,
                                            ITimeProvider timeProvider,
                                            IUnitOfWork unitOfWork)
        {
            _oneTimePasswordQueryService = oneTimePasswordQueryService;
            _secretProvider = secretProvider;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task ResendAsync(Guid oneTimePasswordId, CancellationToken cancellation = default)
        {
            OneTimePassword oneTimePassword = await _oneTimePasswordQueryService.GetOneTimePasswordByIdAsync(oneTimePasswordId, cancellation);
            oneTimePassword.Resend(_secretProvider.Create(),
                                   _timeProvider.NowUnix());

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
