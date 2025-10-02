using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class Entitlement : Entity
    {
        private Entitlement(Guid id,
                            Guid roleId,
                            Guid scopeId)
        {
            RoleId = roleId;
            ScopeId = scopeId;
        }

        internal static Entitlement Create(Guid id,
                                           Guid roleId,
                                           Guid scopeId)
        {
            return new Entitlement(id,
                                   roleId,
                                   scopeId);
        }

        public Guid RoleId { get; private set; }

        public Guid ScopeId { get; private set; }
    }
    #region Ef

    public partial class Entitlement
    {
        private Entitlement()
        {
        }
    }

    #endregion

}
