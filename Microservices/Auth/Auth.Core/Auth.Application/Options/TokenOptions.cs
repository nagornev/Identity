namespace Auth.Application.Options
{
    public abstract class TokenOptions
    {
        public TokenOptions(int lifetime)
        {
            Lifetime = lifetime;
        }

        public int Lifetime { get; }
    }
}
