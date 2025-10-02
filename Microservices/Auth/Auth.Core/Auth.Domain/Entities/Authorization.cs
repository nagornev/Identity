using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class Authorization : Entity
    {
        private List<RolePermission> _rolePermissions = new();

        private List<ScopePermission> _scopePermissions = new();

        private Authorization(Guid id)
        {
            Id = id;
        }

        internal static Authorization Create(Guid id)
        {
            return new Authorization(id);
        }

        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        public IReadOnlyCollection<ScopePermission> ScopePermissions => _scopePermissions.AsReadOnly();

        public IReadOnlyCollection<RolePermission> GetValidRolePermissions()
        {
            return _rolePermissions.Where(x => x.IsValid())
                                   .ToArray();
        }

        public IReadOnlyCollection<ScopePermission> GetValidScopePermissions(long timestamp)
        {
            return _scopePermissions.Where(x => x.IsValidAt(timestamp))
                                    .ToArray();
        }

        public bool HasRolePermission(Func<RolePermission, bool> predicate)
        {
            return _rolePermissions.Any(predicate);
        }

        public bool HasScopePermission(Func<ScopePermission, bool> predicate)
        {
            return _scopePermissions.Any(predicate);
        }

        internal void GrantRolePermission(Guid roleId, long createdAt)
        {
            RolePermission rolePermission = RolePermission.Create(Guid.NewGuid(), Id, roleId, createdAt);
            _rolePermissions.Add(rolePermission);
        }

        internal void GrantScopePermission(Guid scopeId, long createdAt, long? expired = null)
        {
            ScopePermission scopePermission = ScopePermission.Create(Guid.NewGuid(), Id, scopeId, createdAt, expired);
            _scopePermissions.Add(scopePermission);
        }

        internal void RevokeRolePermission(Guid rolePermissionId)
        {
            RolePermission? rolePermission = _rolePermissions.FirstOrDefault(x => x.Id == rolePermissionId);
            rolePermission?.Revoke();
        }

        internal void RevokeScopePermission(Guid scopePermissionId)
        {
            ScopePermission? scopePermission = _scopePermissions.FirstOrDefault(x => x.Id == scopePermissionId);
            scopePermission?.Revoke();
        }

        internal void DeleteRolePermission(RolePermission rolePermission)
        {
            _rolePermissions.Remove(rolePermission);
        }

        internal void DeleteScopePermission(ScopePermission scopePermission)
        {
            _scopePermissions.Remove(scopePermission);
        }
    }

    #region Ef
    public partial class Authorization
    {
        private Authorization()
        {
        }
    }
    #endregion
}
