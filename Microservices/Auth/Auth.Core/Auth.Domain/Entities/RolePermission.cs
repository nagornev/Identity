using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class RolePermission : Entity
    {
        private RolePermission(Guid id,
                               Guid authorizationId,
                               Guid roleId,
                               long createdAt)
        {
            Id = id;
            AuthorizationId = authorizationId;
            RoleId = roleId;
            CreatedAt = createdAt;
        }

        internal static RolePermission Create(Guid id,
                                              Guid authorizationId,
                                              Guid roleId,
                                              long createdAt)
        {
            return new RolePermission(id,
                                      authorizationId,
                                      roleId,
                                      createdAt);
        }

        public Guid AuthorizationId { get; private set; }

        public Guid RoleId { get; private set; }

        public long CreatedAt { get; private set; }

        public bool Revoked { get; private set; }

        public bool IsValid()
        {
            return !Revoked;
        }

        internal void Revoke()
        {
            Revoked = true;
        }
    }

    #region Ef

    public partial class RolePermission
    {
        private RolePermission()
        {
        }
    }

    #endregion
}
