using DDD.Repositories;
using Otp.Application.Abstractions.Services;
using Otp.Application.Exceptions.Applications.OneTimePasswords;
using Otp.Domain.Aggregates;
using Otp.Domain.Specifications;

namespace Otp.Application.Services
{
    public class OneTimePasswordQueryService : IOneTimePasswordQueryService
    {
        private readonly IRepositoryReader<OneTimePassword> _oneTimePasswordRepository;

        public OneTimePasswordQueryService(IRepositoryReader<OneTimePassword> oneTimePasswordRepository)
        {
            _oneTimePasswordRepository = oneTimePasswordRepository;
        }

        public async Task<OneTimePassword> GetOneTimePasswordByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            OneTimePasswordByIdSpecification specification = new OneTimePasswordByIdSpecification(id);

            return await _oneTimePasswordRepository.GetAsync(specification, cancellation) ??
                   throw new OneTimePasswordNotFoundApplicationException(id);
        }

        public async Task<IReadOnlyCollection<OneTimePassword>> GetExpiredOneTimePasswordsAsync(long timestamp)
        {
            OneTimePasswordByExpiredBeforeSpecification specification = new OneTimePasswordByExpiredBeforeSpecification(timestamp);

            return await _oneTimePasswordRepository.FindAsync(specification);
        }
    }
}
