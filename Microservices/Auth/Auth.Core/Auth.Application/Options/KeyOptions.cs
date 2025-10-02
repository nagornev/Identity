namespace Auth.Application.Options
{
    public abstract class KeyOptions
    {
        public KeyOptions(int size,
                          int timeToLive,
                          string rotationInterval)
        {
            Size = size;
            TimeToLive = timeToLive;
            RotationInterval = rotationInterval;
        }

        public int Size { get; }

        public int TimeToLive { get; }

        /// <summary>
        /// Cron format
        /// </summary>
        public string RotationInterval { get; }
    }
}
