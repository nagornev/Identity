using Auth.Domain.ValueObjects;
using DDD.Primitives;

namespace Auth.Domain.Entities
{
    public partial class Authentication : Entity
    {
        private Authentication(Guid id,
                               PasswordHash passwordHash,
                               string passwordSalt)
        {
            Id = id;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        internal static Authentication Create(Guid id,
                                              PasswordHash passwordHash,
                                              string passwordSalt)
        {
            return new Authentication(id,
                                      passwordHash,
                                      passwordSalt);
        }

        public PendingPasswordHash? PendingPasswordHash { get; private set; }

        public PasswordHash PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }

        internal void ChangePassword(PendingPasswordHash pendingPasswordHash)
        {
            PendingPasswordHash = pendingPasswordHash;
        }

        internal void ConfirmPassword()
        {
            PasswordHash = PendingPasswordHash!.PasswordHash;
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
