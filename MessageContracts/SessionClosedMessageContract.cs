namespace MessageContracts
{
    public class SessionClosedMessageContract
    {
        public SessionClosedMessageContract(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
