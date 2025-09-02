namespace Auth.Application.Options
{
    public class ChannelKeyOptions : KeyOptions
    {
        public ChannelKeyOptions(int size, int timeToLive, string rotationInterval) : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
