using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;
using Auth.Domain.Entities;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class DeleteInvalidPermissionsBackgroundService : IDeleteInvalidPermissionsBackgroundService
    {
        private readonly IUserQueryService _userQueryService;

        private readonly ITimeProvider _timeProvider;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteInvalidPermissionsBackgroundService(IUserQueryService userQueryService,
                                                         ITimeProvider timeProvider,
                                                         IUnitOfWork unitOfWork)
        {
            _userQueryService = userQueryService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteInvalidPermissionsAsync(CancellationToken cancellation = default)
        {
            IAsyncEnumerable<User> usersWithInvalidPermissions = _userQueryService.FindUsersWithInvalidPermissionsAsyncStream(_timeProvider.NowUnix());

            await foreach (User user in usersWithInvalidPermissions)
            {
                IEnumerable<RolePermission> invalidRolePermissions = user.Authorization.RolePermissions.Where(x => !x.IsValid());

                foreach (RolePermission invalidRolePermission in invalidRolePermissions)
                {
                    user.DeleteRolePermission(invalidRolePermission.Id);
                }

                IEnumerable<ScopePermission> invalidScopePermissions = user.Authorization.ScopePermissions.Where(x => !x.IsValidAt(_timeProvider.NowUnix()));

                foreach (ScopePermission invalidScopePermission in invalidScopePermissions)
                {
                    user.DeleteScopePermission(invalidScopePermission.Id);
                }

                await _unitOfWork.SaveAsync(cancellation);
            }
        }
    }
}
