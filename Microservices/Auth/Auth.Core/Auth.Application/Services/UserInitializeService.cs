using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Domain.Aggregates;

namespace Auth.Application.Services
{
    public class UserInitializeService : IUserInitializeService
    {
        private readonly IRoleQueryService _roleQueryService;

        private readonly ITimeProvider _timeProvider;

        public UserInitializeService(IRoleQueryService roleQueryService,
                                     ITimeProvider timeProvider)
        {
            _roleQueryService = roleQueryService;
            _timeProvider = timeProvider;
        }
        public async Task Initialize(User user, CancellationToken cancellation)
        {
            Role basicRole = await GetBasicRole(cancellation);

            user.GrantRolePermission(basicRole.Id,
                                     _timeProvider.NowUnix());
        }

        private async Task<Role> GetBasicRole(CancellationToken cancellation = default)
        {
            return await _roleQueryService.GetRoleByNameAsync("Basic", cancellation);
        }
    }
}
