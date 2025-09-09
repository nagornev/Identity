namespace MessageContracts
{
    public class OneTimePasswordCreationRequest
    {
        public OneTimePasswordCreationRequest(Guid subject, string tag, string payload)
        {
            Subject = subject;
            Tag = tag;
            Payload = payload;
        }

        public Guid Subject { get; }

        public string Tag { get; }

        public string Payload { get; }
    }
}
