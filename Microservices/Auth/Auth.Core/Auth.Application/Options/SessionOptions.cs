namespace Auth.Application.Options
{
    public class SessionOptions
    {
        public SessionOptions(int lifetime)
        {
            Lifetime = lifetime;
        }

        public int Lifetime { get; }
    }
}
