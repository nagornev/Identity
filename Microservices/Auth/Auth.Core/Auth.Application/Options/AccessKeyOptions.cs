namespace Auth.Application.Options
{
    public class AccessKeyOptions : KeyOptions
    {
        public AccessKeyOptions(int size,
                                int timeToLive,
                                int rotationInterval)
            : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
