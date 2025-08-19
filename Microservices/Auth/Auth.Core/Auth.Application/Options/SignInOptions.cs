namespace Auth.Application.Options
{
    public class SignInOptions
    {
        public SignInOptions(int window)
        {
            Window = window;
        }

        public int Window { get; }
    }
}
