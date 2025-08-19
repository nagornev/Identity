using Auth.Application.Abstractions.Providers;
using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Scopes;
using Auth.Domain.Aggregates;
using Auth.Domain.ValueObjects;
using System.Data;

namespace Auth.Application.Services
{
    public class UserScopesService : IUserScopesService
    {
        private readonly IRoleQueryService _roleQueryService;

        private readonly IScopeQueryService _scopeQueryService;

        private readonly ITimeProvider _timeProvider;

        public UserScopesService(IRoleQueryService roleQueryService,
                                 IScopeQueryService scopeQueryService,
                                 ITimeProvider timeProvider)
        {
            _roleQueryService = roleQueryService;
            _scopeQueryService = scopeQueryService;
            _timeProvider = timeProvider;
        }

        public async Task<IReadOnlyCollection<Scope>> GetUserScopesAsync(User user, Audience audience, CancellationToken cancellation = default)
        {
            IReadOnlyCollection<Role> roles = await _roleQueryService.GetRolesByIdsAsync(user.Authorization.RolePermissions.Where(x => x.IsValid())
                                                                                                                           .Select(x => x.RoleId)
                                                                                                                           .ToArray(),
                                                                                         cancellation);

            IReadOnlyCollection<Scope> scopes = await _scopeQueryService.GetScopesByIdsAsync(roles.SelectMany(x => x.Entitlements
                                                                                                  .Select(e => e.ScopeId))
                                                                                                  .Distinct()
                                                                                                  .Union(user.Authorization.ScopePermissions
                                                                                                                           .Where(x => x.IsValidAt(_timeProvider.NowUnix()))
                                                                                                                           .Select(x => x.Id))
                                                                                                  .ToArray(),
                                                                                             cancellation);

            IReadOnlyCollection<Scope> audienceUserScopes = scopes.Where(x => x.Audience == audience)
                                                                  .ToArray();

            return audienceUserScopes.Count > 0 ?
                   audienceUserScopes :
                   throw new ScopesNotFoundApplicationException(audience.Value);
        }
    }
}
