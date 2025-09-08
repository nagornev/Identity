namespace MessageContracts
{
    public class SessionRevokedMessageContract : IMessageContract
    {
        public SessionRevokedMessageContract(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
