using Auth.Domain.Events;
using Auth.Domain.Exceptions.Domains.Scopes;
using Auth.Domain.Exceptions.Domains.Sessions;
using Auth.Domain.ValueObjects;
using DDD.Primitives;

namespace Auth.Domain.Aggregates
{
    public partial class Session : AggregateRoot
    {
        private Session(Guid id,
                        Guid userId,
                        Guid kid,
                        Guid version,
                        string publicKey,
                        Audience audience,
                        Device device,
                        IpAddress ipAddress,
                        long createdAt,
                        long expiresAt,
                        bool activated)
        {
            Id = id;
            UserId = userId;
            Kid = kid;
            Version = version;
            PublicKey = publicKey;
            Audience = audience;
            Device = device;
            IpAddress = ipAddress;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
            Activated = activated;
        }

        /// <summary>
        /// Creates new session.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="kid"></param>
        /// <param name="publicKey"></param>
        /// <param name="audience"></param>
        /// <param name="device"></param>
        /// <param name="ipAddress"></param>
        /// <param name="createdAt"></param>
        /// <param name="expiresAt"></param>
        /// <returns></returns>
        /// <exception cref="AudienceNullDomainException"></exception>
        /// <exception cref="DeviceNullDomainException"></exception>
        /// <exception cref="IpAddressNullDomainException"></exception>
        public static Session Create(Guid userId,
                                     Guid kid,
                                     string publicKey,
                                     string audience,
                                     string device,
                                     string ipAddress,
                                     long createdAt,
                                     long expiresAt)
        {
            Guid id = Guid.NewGuid();
            Guid version = Guid.NewGuid();

            Session session = new Session(id,
                                          userId,
                                          kid,
                                          version,
                                          publicKey,

                                          Audience.Create(audience) ??
                                          throw new AudienceNullDomainException(),

                                          Device.Create(device) ??
                                          throw new DeviceNullDomainException(),

                                          IpAddress.Create(ipAddress) ??
                                          throw new IpAddressNullDomainException(),

                                          createdAt,
                                          expiresAt,
                                          false);

            session.AddDomainEvent(new SessionCreatedDomainEvent(session.Id, session.Kid, session.Version, session.PublicKey));

            return session;
        }

        public Guid UserId { get; private set; }

        public Guid Kid { get; private set; }

        public Guid Version { get; private set; }

        public string PublicKey { get; private set; }

        public Audience Audience { get; private set; }

        public Device Device { get; private set; }

        public IpAddress IpAddress { get; private set; }

        public long CreatedAt { get; private set; }

        public long ExpiresAt { get; private set; }

        public long UpdatedAt { get; private set; }

        public bool Closed { get; private set; }

        public bool Revoked { get; private set; }

        public bool Activated { get; private set; }

        public bool Deleted { get; private set; }

        public bool IsValidAt(long timestamp)
        {
            if (!Activated || Closed || Revoked || Deleted)
                return false;

            return ExpiresAt >= timestamp;
        }

        /// <summary>
        /// Changes the session <paramref name="kid"/>.
        /// </summary>
        /// <param name="kid"></param>
        public void ChangeKidIfNeed(Guid kid)
        {
            if (Kid == kid)
                return;

            Kid = kid;
        }

        /// <summary>
        /// Updates the session <paramref name="publicKey"/> and version.
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="updatedAt"></param>
        /// <exception cref="UpdatedAtOutOfRangeDomainException"></exception>
        public void Update(string publicKey, long updatedAt)
        {
            if (UpdatedAt >= updatedAt)
                throw new UpdatedAtOutOfRangeDomainException(UpdatedAt);

            if (PublicKey == publicKey)
                throw new PublicKeyInvalidDomainException();

            Version = Guid.NewGuid();
            PublicKey = publicKey;
            UpdatedAt = updatedAt;

            AddDomainEvent(new SessionUpdatedDomainEvent(Id, Kid, Version, Device.Value, IpAddress.Value));
        }

        /// <summary>
        /// Closes the session.
        /// </summary>
        /// <exception cref="SessionAlreadyClosedDomainException"></exception>
        public void Close()
        {
            if (Closed)
                throw new SessionAlreadyClosedDomainException();

            Closed = true;

            AddDomainEvent(new SessionClosedDomainEvent(Id));
        }

        /// <summary>
        /// Revokes the session.
        /// </summary>
        /// <exception cref="SessionAlreadyRevokedDomainException"></exception>
        public void Revoke()
        {
            if (Revoked)
                throw new SessionAlreadyRevokedDomainException();

            Revoked = true;

            AddDomainEvent(new SessionRevokedDomainEvent(Id));
        }

        /// <summary>
        /// Activates the session.
        /// </summary>
        /// <exception cref="SessionAlreadyActivatedDomainException"></exception>
        public void Activate()
        {
            if (Activated)
                throw new SessionAlreadyActivatedDomainException();

            Activated = true;
        }

        /// <summary>
        /// Marks the session for delete.
        /// </summary>
        /// <exception cref="SessionAlreadyDeletedDomainException"></exception>
        public void MarkAsDeleted()
        {
            if (Deleted)
                throw new SessionAlreadyDeletedDomainException();

            Deleted = true;

            AddDomainEvent(new SessionDeletedDomainEvent(Id));
        }
    }

    #region Ef

    public partial class Session
    {
        private Session()
        {
        }
    }

    #endregion
}
