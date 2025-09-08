namespace Otp.Application.Options
{
    public class SecretOptions
    {
        public SecretOptions(int size)
        {
            Size = size;
        }

        public int Size { get; }
    }
}
