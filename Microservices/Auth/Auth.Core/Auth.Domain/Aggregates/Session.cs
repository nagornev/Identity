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
                        bool isActive)
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
            IsActive = isActive;
        }

        /// <summary>
        /// Creates new session.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="kid"></param>
        /// <param name="device"></param>
        /// <param name="ipAddress"></param>
        /// <param name="createdAt"></param>
        /// <param name="expiresAt"></param>
        /// <returns></returns>
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

        public bool Deleted { get; private set; }

        public bool Revoked { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsValidAt(long timestamp)
        {
            if (!IsActive || Closed || Revoked || Deleted)
                return false;

            return ExpiresAt >= timestamp;
        }

        public void ChangeKidIfNeed(Guid kid)
        {
            if (Kid == kid)
                return;

            Kid = kid;

            AddDomainEvent(new SessionKidChangedDomainEvent(Id, Kid));
        }

        public void UpdateSession(string publicKey, long updatedAt)
        {
            if (UpdatedAt >= updatedAt)
                throw new UpdatedAtOutOfRangeDomainException(UpdatedAt);

            Version = Guid.NewGuid();
            PublicKey = publicKey;
            UpdatedAt = updatedAt;

            AddDomainEvent(new SessionUpdatedDomainEvent(Id, Kid, Version, Device.Value, IpAddress.Value));
        }

        public void CloseSession()
        {
            Closed = true;

            AddDomainEvent(new SessionClosedDomainEvent(Id));
        }

        public void RevokeSession()
        {
            Revoked = true;

            AddDomainEvent(new SessionRevokedDomainEvent(Id));
        }

        public void MarkAsDeleted()
        {
            Deleted = true;

            AddDomainEvent(new SessionDeletedDomainEvent(Id));
        }

        public void Activate()
        {
            IsActive = true;
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
