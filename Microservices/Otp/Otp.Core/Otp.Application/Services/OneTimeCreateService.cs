using DDD.Repositories;
using Otp.Application.Abstractions.Factories;
using Otp.Application.Abstractions.Services;
using Otp.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otp.Application.Services
{
    public class OneTimeCreateService : IOneTimeCreateService
    {
        private readonly IOneTimePasswordFactory _oneTimePasswordFactory;

        private readonly IRepositoryWriter<OneTimePassword> _oneTimePasswordRepository;

        private readonly IUnitOfWork _unitOfWork;

        public OneTimeCreateService(IOneTimePasswordFactory oneTimePasswordFactory,
                                    IRepositoryWriter<OneTimePassword> oneTimePasswordRepository,
                                    IUnitOfWork unitOfWork)
        {
            _oneTimePasswordFactory = oneTimePasswordFactory;
            _oneTimePasswordRepository = oneTimePasswordRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(string tag, Guid subject, string? payload = "", CancellationToken cancellation = default)
        {
            OneTimePassword oneTimePassword = _oneTimePasswordFactory.Create(tag, subject, payload);

            await _oneTimePasswordRepository.AddAsync(oneTimePassword);
            await _unitOfWork.SaveAsync(cancellation);

            return oneTimePassword.Id;
        }
    }
}
