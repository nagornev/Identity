using Auth.Domain.Entities;
using Auth.Domain.Events;
using Auth.Domain.Exceptions.Domains.Users;
using Auth.Domain.ValueObjects;
using DDD.Primitives;
using System.Threading.Tasks;

namespace Auth.Domain.Aggregates
{
    public partial class User : AggregateRoot
    {
        private User(Guid id,
                     Authentication authentication,
                     Authorization authorization,
                     Profile profile,
                     long createdAt,
                     bool isActive)
        {
            Id = id;
            Authentication = authentication;
            Authorization = authorization;
            Profile = profile;
            CreatedAt = createdAt;
            IsActive = isActive;
        }

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="passwordHash"></param>
        /// <param name="personName"></param>
        /// <param name="tokenVersion"></param>
        /// <param name="createdAt"></param>
        /// <returns></returns>
        public static User Create(string emailAddress,
                                  string personName,
                                  string passwordHash,
                                  long createdAt)
        {
            Guid id = Guid.NewGuid();

            User user = new User(id,
                                 Authentication.Create(id,
                                                       PasswordHash.Create(passwordHash) ??
                                                       throw new PasswordHashNullDomainException()) ??
                                 throw new AuthenticationNullDomainException(),

                                 Authorization.Create(id) ??
                                 throw new AuthorizationNullDomainException(),

                                 Profile.Create(id,
                                                EmailAddress.Create(emailAddress) ??
                                                throw new EmailAddressNullDomainException(),

                                                PersonName.Create(personName) ??
                                                throw new PersonNameNullDomainException()) ??
                                 throw new ProfileNullDomainException(),

                                 createdAt,
                                 false);

            user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, user.Profile.EmailAddress.Value));

            return user;
        }


        public Authentication Authentication { get; private set; }

        public Authorization Authorization { get; private set; }

        public Profile Profile { get; private set; }

        public long CreatedAt { get; private set; }

        public bool IsActive { get; private set; }

        /// <summary>
        /// Changes user`s password hash.
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <exception cref="PasswordHashNullDomainException"></exception>
        /// <exception cref="PasswordHashAlreadySetDomainException"></exception>
        public void ChangePassword(string passwordHash)
        {
            PasswordHash password = PasswordHash.Create(passwordHash) ??
                                    throw new PasswordHashNullDomainException();

            if (Authentication.PasswordHash == password)
                throw new PasswordHashAlreadySetDomainException();

            Authentication.ChangePassword(password);
        }

        /// <summary>
        /// Confirms user`s password hash.
        /// </summary>
        /// <exception cref="PasswordHashNullDomainException"></exception>
        public void ConfirmPassword()
        {
            if (Authentication.PendingPasswordHash is null)
                throw new PasswordHashNullDomainException();

            Authentication.ConfirmPassword();

            AddDomainEvent(new PasswordHashChangedDomainEvent(Id));
        }

        /// <summary>
        /// Changes user`s email address (sets pending email address).
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="EmailAddressNullDomainException"></exception>
        /// <exception cref="EmailAddressAlreadySetDomainException"></exception>
        public void ChangeEmailAddress(string email)
        {
            EmailAddress emailAddress = EmailAddress.Create(email) ??
                                        throw new EmailAddressNullDomainException();

            if (Profile.EmailAddress == emailAddress)
                throw new EmailAddressAlreadySetDomainException(email);

            Profile.ChangeEmailAddress(emailAddress);
        }

        /// <summary>
        /// Confirms user`s email address.
        /// </summary>
        /// <exception cref="EmailAddressNullDomainException"></exception>
        public void ConfirmEmailAddressChange()
        {
            if (Profile.PendingEmailAddress is null)
                throw new EmailAddressNullDomainException();

            Profile.ConfirmEmailAddressChange();

            AddDomainEvent(new EmailAddressChangeConfirmedDomainEvent(Id));
        }

        /// <summary>
        /// Updates user`s email address.
        /// </summary>
        /// <exception cref="EmailAddressNullDomainException"></exception>
        /// <exception cref="EmailAddressChangeUnconfirmedDomainException"></exception>
        public void UpdateEmailAddress()
        {
            if (Profile.PendingEmailAddress is null)
                throw new EmailAddressNullDomainException();

            if (!Profile.IsEmailAddressChangeConfirmed)
                throw new EmailAddressChangeUnconfirmedDomainException();

            Profile.UpdateEmailAddress();

            AddDomainEvent(new EmailAddressChangedDomainEvent(Id, Profile.EmailAddress.Value));
        }

        /// <summary>
        /// Changes the username in the profile.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="PersonNameNullDomainException"></exception>
        public void ChangePersonName(string name)
        {
            PersonName personName = PersonName.Create(name) ??
                                    throw new PersonNameNullDomainException();

            Profile.ChangePersonName(personName);
        }

        /// <summary>
        /// Grantes the role permission.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="createdAt"></param>
        /// <exception cref="PermissionAlreadyExistsDomainException"></exception>
        public void GrantRolePermission(Guid roleId, long createdAt)
        {
            if (Authorization.HasRolePermission(x => x.RoleId == roleId &&
                                                     x.IsValid()))
                throw new PermissionAlreadyExistsDomainException();

            Authorization.GrantRolePermission(roleId, createdAt);
        }

        /// <summary>
        /// Revokes the role permission.
        /// </summary>
        /// <param name="rolePermissionId"></param>
        /// <exception cref="PermissionNotFoundDomainException"></exception>
        public void RevokeRolePermission(Guid rolePermissionId)
        {
            if (!Authorization.HasRolePermission(x => x.Id == rolePermissionId))
                throw new PermissionNotFoundDomainException();

            Authorization.RevokeRolePermission(rolePermissionId);
        }

        /// <summary>
        /// Deletes the role permission.
        /// </summary>
        /// <param name="rolePermissionId"></param>
        /// <exception cref="PermissionNotFoundDomainException"></exception>
        public void MarkAsDeletedRolePermission(Guid rolePermissionId)
        {
            if (!Authorization.HasRolePermission(x => x.Id == rolePermissionId))
                throw new PermissionNotFoundDomainException();

            RolePermission rolePermission = Authorization.RolePermissions.FirstOrDefault(x => x.Id == rolePermissionId)!;

            Authorization.MarkAsDeletedRolePermission(rolePermission.Id);

            AddDomainEvent(new RolePermissionDeletedDomainEvent(Id, rolePermission.RoleId));
        }

        /// <summary>
        /// Grantes the scope permission.
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="createdAt"></param>
        /// <param name="expiresAt"></param>
        /// <exception cref="PermissionAlreadyExistsDomainException"></exception>
        public void GrantScopePermission(Guid scopeId, long createdAt, long? expiresAt = null)
        {
            if (Authorization.HasScopePermission(x => x.ScopeId == scopeId &&
                                                     x.IsValidAt(createdAt)))
                throw new PermissionAlreadyExistsDomainException();

            Authorization.GrantScopePermission(scopeId, createdAt, expiresAt);
        }

        /// <summary>
        /// Revokes the scope permission.
        /// </summary>
        /// <param name="scopePermissionId"></param>
        /// <exception cref="PermissionNotFoundDomainException"></exception>
        public void RevokeScopePermission(Guid scopePermissionId)
        {
            if (!Authorization.HasScopePermission(x => x.Id == scopePermissionId))
                throw new PermissionNotFoundDomainException();

            Authorization.RevokeScopePermission(scopePermissionId);
        }

        /// <summary>
        /// Deletes the scope permission.
        /// </summary>
        /// <param name="scopePermissionId"></param>
        /// <exception cref="PermissionNotFoundDomainException"></exception>
        public void MarkAsDeletedScopePermission(Guid scopePermissionId)
        {
            if (!Authorization.HasScopePermission(x => x.Id == scopePermissionId))
                throw new PermissionNotFoundDomainException();

            ScopePermission scopePermission = Authorization.ScopePermissions.FirstOrDefault(x => x.ScopeId == scopePermissionId)!;

            Authorization.RevokeScopePermission(scopePermissionId);

            AddDomainEvent(new ScopePermissionDeletedDomainEvent(Id, scopePermission.ScopeId));
        }

        /// <summary>
        /// Activates the user account.
        /// </summary>
        public void Activate()
        {
            if (IsActive)
                throw new UserAlreadyActiveDomainException(Id);

            IsActive = true;
        }

        /// <summary>
        /// Deactivates the user account.
        /// </summary>
        public void Deactivate()
        {
            if (!IsActive)
                throw new UserAlreadyDeactiveDomainException(Id);

            IsActive = false;
        }
    }

    #region Ef

    public partial class User
    {
        private User()
        {
        }
    }

    #endregion
}
