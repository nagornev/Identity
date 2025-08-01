namespace Auth.Application.Options
{
    public abstract class KeyOptions
    {
        public KeyOptions(int size,
                          int timeToLive,
                          int rotationInterval)
        {
            Size = size;
            TimeToLive = timeToLive;
            RotationInterval = rotationInterval;
        }

        public int Size { get; }

        public int TimeToLive { get; }

        public int RotationInterval { get; }
    }
}
