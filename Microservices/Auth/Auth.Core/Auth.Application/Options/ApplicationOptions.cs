namespace Auth.Application.Options
{
    public class ApplicationOptions
    {
        public ApplicationOptions(string issuer, string basicRoleName, int unactivatedUsersLifetime)
        {
            Issuer = issuer;
            BasicRoleName = basicRoleName;
            UnactivatedUsersLifetime = unactivatedUsersLifetime;
        }

        public string Issuer { get; }

        public string BasicRoleName { get; }

        public int UnactivatedUsersLifetime { get; }
    }
}
