namespace Auth.Application.Options
{
    public class EmailKeyOptions : KeyOptions
    {
        public EmailKeyOptions(int size,
                               int timeToLive,
                               int rotationInterval)
            : base(size, timeToLive, rotationInterval)
        {
        }
    }
}
