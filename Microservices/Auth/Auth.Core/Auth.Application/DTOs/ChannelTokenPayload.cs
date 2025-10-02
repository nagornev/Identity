namespace Auth.Application.DTOs
{
    public class ChannelTokenPayload
    {
        public ChannelTokenPayload(Guid userId,
                                   string tag,
                                   string channel)
        {
            UserId = userId;
            Tag = tag;
            Channel = channel;
        }

        public Guid UserId { get; }

        public string Tag { get; }

        public string Channel { get; }
    }
}
