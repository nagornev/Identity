using DDD.Repositories;
using Otp.Application.Abstractions.Providers;
using Otp.Application.Abstractions.Services;
using Otp.Application.DTOs;
using Otp.Domain.Aggregates;

namespace Otp.Application.Services
{
    public class OneTimePasswordValidationService : IOneTimePasswordValidationService
    {
        private readonly IOneTimePasswordQueryService _oneTimePasswordQueryService;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public OneTimePasswordValidationService(IOneTimePasswordQueryService oneTimePasswordQueryService,
                                                ITimeProvider timeProvider,
                                                IUnitOfWork unitOfWork)
        {
            _oneTimePasswordQueryService = oneTimePasswordQueryService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<OneTimePasswordValidation> ValidateAsync(Guid oneTimePasswordId, string oneTimePasswordValue, string tag, CancellationToken cancellation = default)
        {
            OneTimePassword oneTimePassword = await _oneTimePasswordQueryService.GetOneTimePasswordByIdAsync(oneTimePasswordId, cancellation);

            bool isValid = oneTimePassword.ValidateAt(oneTimePasswordValue, tag, _timeProvider.NowUnix());

            await _unitOfWork.SaveAsync(cancellation);

            return isValid ?
                    new OneTimePasswordValidation(true, oneTimePassword.UserId, oneTimePassword.Payload) :
                    new OneTimePasswordValidation(false, Guid.NewGuid());
        }
    }
}
