namespace MessageContracts
{
    public class SessionRevokedMessageContract
    {
        public SessionRevokedMessageContract(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
