namespace Auth.Application.DTOs
{
    public class ChannelTokenCreationParameters
    {
        public ChannelTokenCreationParameters(Guid userId,
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
