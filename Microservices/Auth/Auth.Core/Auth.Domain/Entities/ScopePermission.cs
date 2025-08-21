using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class ScopePermission : Entity
    {
        private ScopePermission(Guid id,
                                Guid authorizationId,
                                Guid scopeId,
                                long createdAt,
                                long? expiresAt)
        {
            Id = id;
            AuthorizationId = authorizationId;
            ScopeId = scopeId;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        internal static ScopePermission Create(Guid id,
                                               Guid authorizationId,
                                               Guid scopeId,
                                               long createdAt,
                                               long? expiresAt = null)
        {
            return new ScopePermission(id,
                                       authorizationId,
                                       scopeId,
                                       createdAt,
                                       expiresAt);
        }

        public Guid AuthorizationId { get; private set; }

        public Guid ScopeId { get; private set; }

        public long CreatedAt { get; private set; }

        public long? ExpiresAt { get; private set; }

        public bool Revoked { get; private set; }

        public bool IsValidAt(long timestamp)
        {
            if (Revoked)
                return false;

            if (ExpiresAt.HasValue)
                return ExpiresAt > timestamp;

            return true;
        }

        internal void Revoke()
        {
            Revoked = true;
        }
    }

    #region Ef
    public partial class ScopePermission
    {
        private ScopePermission()
        {
        }
    }
    #endregion
}
