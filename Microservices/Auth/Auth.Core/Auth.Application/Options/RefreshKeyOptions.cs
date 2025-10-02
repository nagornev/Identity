namespace Auth.Application.Options
{
    public class RefreshKeyOptions : KeyOptions
    {
        public RefreshKeyOptions(int size, int timeToLive, string rotationInterval) : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
