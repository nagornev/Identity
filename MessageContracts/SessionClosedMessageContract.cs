namespace MessageContracts
{
    public class SessionClosedMessageContract : IMessageContract
    {
        public SessionClosedMessageContract(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
