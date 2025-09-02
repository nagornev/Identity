namespace Auth.Application.Options
{
    public class AccessKeyOptions : KeyOptions
    {
        public AccessKeyOptions(int size, int timeToLive, string rotationInterval) : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
