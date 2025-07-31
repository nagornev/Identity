using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class PersonNameService : IPersonNameService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly IUnitOfWork _unitOfWork;

        public PersonNameService(IUserQueryService userQueryService,
                                 IUnitOfWork unitOfWork)
        {
            _userQueryService = userQueryService;
            _unitOfWork = unitOfWork;
        }

        public async Task ChangeAsync(Guid userId, string personName, CancellationToken cancellation = default)
        {
            User user = await _userQueryService.GetUserByIdAsync(userId, cancellation);
            user.ChangePersonName(personName);

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
