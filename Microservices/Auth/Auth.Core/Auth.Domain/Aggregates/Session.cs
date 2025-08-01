using Auth.Domain.Events;
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
                        Device device,
                        IpAddress ipAddress,
                        long createdAt,
                        long expiresAt)
        {
            Id = id;
            UserId = userId;
            Kid = kid;
            Version = version;
            Device = device;
            IpAddress = ipAddress;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
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

                                          Device.Create(device) ?? 
                                          throw new DeviceNullDomainException(),

                                          IpAddress.Create(ipAddress) ?? 
                                          throw new IpAddressNullDomainException(),

                                          createdAt,
                                          expiresAt);

            session.AddDomainEvent(new SessionCreatedDomainEvent(id, kid));

            return session;
        }

        public Guid UserId { get; private set; }

        public Guid Kid { get; private set; }

        public Guid Version { get; private set; }

        public Device Device { get; private set; }

        public IpAddress IpAddress { get; private set; }

        public long CreatedAt { get; private set; }

        public long ExpiresAt { get; private set; }

        public long UpdatedAt { get; private set; }

        public bool Closed { get; private set; }

        public bool Deleted { get; private set; }

        public bool IsValidAt(long timestamp)
        {
            if (Closed || Deleted)
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

        public void UpdateSession(string device, string ipAddress, long updatedAt)
        {
            if (UpdatedAt >= updatedAt)
                throw new UpdatedAtOutOfRangeDomainException(UpdatedAt);

            Version = Guid.NewGuid();
            Device = Device.Create(device) ?? throw new DeviceNullDomainException();
            IpAddress = IpAddress.Create(ipAddress) ?? throw new IpAddressNullDomainException();
            UpdatedAt = updatedAt;
        
            AddDomainEvent(new SessionUpdatedDomainEvent(Id, Kid, Version, Device.Value, IpAddress.Value));
        }

        public void CloseSession()
        {
            Closed = true;

            AddDomainEvent(new SessionClosedDomainEvent(Id));
        }

        public void MarkAsDeleted()
        {
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
