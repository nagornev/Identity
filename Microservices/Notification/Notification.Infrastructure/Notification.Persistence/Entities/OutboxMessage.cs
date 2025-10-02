namespace Notification.Persistence.Entities
{
    public partial class OutboxMessage
    {
        public const int LocktimeDefault = 60;

        public OutboxMessage(Guid id,
                             Guid streamId,
                             string type,
                             string payload,
                             long createdAt,
                             int locktime,
                             long? lockedAt,
                             long? lockedUntil,
                             bool processed)
        {
            Id = id;
            StreamId = streamId;
            Type = type;
            Payload = payload;
            CreatedAt = createdAt;
            Locktime = locktime;
            LockedAt = lockedAt;
            LockedUntil = lockedUntil;
            Processed = processed;
        }

        internal static OutboxMessage Create(Guid streamId,
                                             string type,
                                             string payload,
                                             long createdAt,
                                             int locktime = LocktimeDefault)
        {
            Guid id = Guid.NewGuid();

            return new OutboxMessage(id,
                                     streamId,
                                     type,
                                     payload,
                                     createdAt,
                                     locktime,
                                     default,
                                     default,
                                     false);
        }

        public Guid Id { get; private set; }

        public Guid StreamId { get; private set; }

        public string Type { get; private set; }

        public string Payload { get; private set; }

        public long CreatedAt { get; private set; }

        public int Locktime { get; private set; }

        public long? LockedAt { get; private set; }

        public long? LockedUntil { get; private set; }

        public bool Processed { get; private set; }

        public void MarkAsProccesed()
        {
            LockedAt = null;
            LockedUntil = null;
            Processed = true;
        }
    }

    #region Ef
    public partial class OutboxMessage
    {
        private OutboxMessage()
        {
        }
    }
    #endregion   
}
