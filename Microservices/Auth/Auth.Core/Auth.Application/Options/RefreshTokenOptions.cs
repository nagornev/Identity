namespace Auth.Application.Options
{
    public class RefreshTokenOptions : TokenOptions
    {
        public RefreshTokenOptions(int lifetime) : base(lifetime)
        {
        }
    }
}
