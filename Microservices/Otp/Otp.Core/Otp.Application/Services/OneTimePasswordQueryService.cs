using DDD.Repositories;
using Otp.Application.Abstractions.Services;
using Otp.Application.Exceptions.Applications.OneTimePasswords;
using Otp.Domain.Aggregates;
using Otp.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IAsyncEnumerable<OneTimePassword> GetExpiredOneTimePasswordsAsyncEnumerable(long timestamp)
        {
            OneTimePasswordByExpiredBeforeSpecification specification = new OneTimePasswordByExpiredBeforeSpecification(timestamp);

            return _oneTimePasswordRepository.AsyncStream(specification);
        }
    }
}
