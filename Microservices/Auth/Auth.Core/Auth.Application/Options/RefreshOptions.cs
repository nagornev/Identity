namespace Auth.Application.Options
{
    public class RefreshOptions
    {
        public RefreshOptions(int window)
        {
            Window = window;
        }

        public int Window { get; }
    }
}
