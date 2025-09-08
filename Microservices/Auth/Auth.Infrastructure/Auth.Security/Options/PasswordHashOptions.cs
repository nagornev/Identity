namespace Auth.Security.Options
{
    public class PasswordHashOptions
    {
        public PasswordHashOptions(int size,
                                   int iterations)
        {
            Size = size;
            Iterations = iterations;
        }

        public int Size { get; }

        public int Iterations { get; }
    }
}
