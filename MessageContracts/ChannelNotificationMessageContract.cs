namespace MessageContracts
{
    public class ChannelNotificationMessageContract
    {
        public ChannelNotificationMessageContract(Guid userId,
                                                  string channel,
                                                  string token)
        {
            UserId = userId;
            Channel = channel;
            Token = token;
        }

        public Guid UserId { get; }

        public string Channel { get; }

        public string Token { get; }
    }
}
