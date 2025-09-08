using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using DDD.Repositories;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class DeleteUnactivatedUsersBackgroundService : IDeleteUnactivatedUsersBackgroundService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly ITimeProvider _timeProvider;

        private readonly ApplicationOptions _applicationOptions;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteUnactivatedUsersBackgroundService(IUserQueryService userQueryService,
                                                       ITimeProvider timeProvider,
                                                       IOptions<ApplicationOptions> applicationOptions,
                                                       IUnitOfWork unitOfWork)
        {
            _userQueryService = userQueryService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
            _applicationOptions = applicationOptions.Value;
        }

        public async Task DeleteUnactivatedUsersAsync(CancellationToken cancellation = default)
        {
            long timestamp = _timeProvider.NowUnix() - _applicationOptions.UnactivatedUsersLifetime;

            IAsyncEnumerable<User> unactivatedUsers = _userQueryService.FindUnactivatedUsersAsyncStream(timestamp);

            await foreach (User unactivatedUser in unactivatedUsers)
            {
                unactivatedUser.MarkAsDeleted();
            }

            await _unitOfWork.SaveAsync(cancellation);
        }
    }
}
