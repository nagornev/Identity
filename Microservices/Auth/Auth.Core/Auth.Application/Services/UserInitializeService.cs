using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Domain.Aggregates;
using Microsoft.Extensions.Options;

namespace Auth.Application.Services
{
    public class UserInitializeService : IUserInitializeService
    {
        private readonly IRoleQueryService _roleQueryService;

        private readonly ITimeProvider _timeProvider;

        private readonly ApplicationOptions _applicationOptions;

        public UserInitializeService(IRoleQueryService roleQueryService,
                                     ITimeProvider timeProvider,
                                     IOptions<ApplicationOptions> applicationOptions)
        {
            _roleQueryService = roleQueryService;
            _timeProvider = timeProvider;
            _applicationOptions = applicationOptions.Value;
        }
        public async Task Initialize(User user, CancellationToken cancellation)
        {
            Role basicRole = await GetBasicRole(cancellation);

            user.GrantRolePermission(basicRole.Id,
                                     _timeProvider.NowUnix());
        }

        private async Task<Role> GetBasicRole(CancellationToken cancellation = default)
        {
            return await _roleQueryService.GetRoleByNameAsync(_applicationOptions.Roles.Basic.Name, cancellation);
        }
    }
}
