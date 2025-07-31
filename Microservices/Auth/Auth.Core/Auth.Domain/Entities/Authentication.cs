using Auth.Domain.ValueObjects;
using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class Authentication : Entity
    {
        private Authentication(Guid id,
                               PasswordHash passwordHash)
        {
            Id = id;
            PasswordHash = passwordHash;
        }

        internal static Authentication Create(Guid id,
                                              PasswordHash passwordHash)
        {
            return new Authentication(id,
                                      passwordHash);
        }

        public PasswordHash? PendingPasswordHash { get; private set; }

        public PasswordHash PasswordHash { get; private set; }

        internal void ChangePassword(PasswordHash passwordHash)
        {
            PendingPasswordHash = passwordHash;
        }

        internal void ConfirmPassword()
        {
            PasswordHash = PendingPasswordHash!;
            PendingPasswordHash = null;
        }
    }

    #region Ef
    public partial class Authentication
    {
        private Authentication()
        {
        }
    }
    #endregion
}
