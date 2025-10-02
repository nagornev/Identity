using Auth.Domain.Entities;
using Auth.Domain.Exceptions.Domains.Roles;
using DDD.Primitives;

namespace Auth.Domain.Aggregates
{
    public partial class Role : AggregateRoot
    {
        private List<Entitlement> _entitlements = new();

        private Role(Guid id,
                     string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Creates new role.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Role Create(string name)
        {
            Guid id = Guid.NewGuid();

            return new Role(id,
                            name);
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<Entitlement> Entitlements => _entitlements.AsReadOnly();

        /// <summary>
        /// Сhecks the contents <paramref name="scopeId"/> in the role.
        /// </summary>
        /// <param name="scopeId"></param>
        /// <returns></returns>
        public bool HasEntitlement(Func<Entitlement, bool> predicate)
        {
            return _entitlements.Any(predicate);
        }

        /// <summary>
        /// Assingns the new entitlement to the role.
        /// </summary>
        /// <param name="scopeId"></param>
        public void AssignEntitlement(Guid scopeId)
        {
            if (HasEntitlement(x => x.ScopeId == scopeId))
                throw new EntitlementAreadyExistsDomainException(scopeId);

            Entitlement entitlement = Entitlement.Create(Guid.NewGuid(), Id, scopeId);
            _entitlements.Add(entitlement);
        }

        /// <summary>
        /// Revokes the entitlement from the role.
        /// </summary>
        /// <param name="entitlementId"></param>
        public void RevokeEntitlement(Guid entitlementId)
        {
            if (!HasEntitlement(x => x.Id == entitlementId))
                throw new EntitlementNotFoundDomainException(entitlementId);

            Entitlement entitlement = _entitlements.First(x => x.Id == entitlementId);
            _entitlements.Remove(entitlement);
        }
    }

    #region Ef
    public partial class Role
    {
        private Role()
        {

        }
    }
    #endregion
}
