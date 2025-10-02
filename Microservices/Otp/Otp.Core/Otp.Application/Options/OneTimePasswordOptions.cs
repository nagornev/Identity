namespace Otp.Application.Options
{
    public class OneTimePasswordOptions
    {
        public OneTimePasswordOptions(int lifetime, string deletionInterval)
        {
            Lifetime = lifetime;
            DeletionInterval = deletionInterval;
        }

        public int Lifetime { get; }

        public string DeletionInterval { get; }
    }
}
