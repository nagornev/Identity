namespace Auth.Application.Options
{
    public class SaltOptions
    {
        public SaltOptions(int size)
        {
            Size = size;
        }

        public int Size { get; }
    }
}
