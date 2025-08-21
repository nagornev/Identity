namespace Auth.Application.Options
{
    public class ApplicationOptions
    {
        public ApplicationOptions(string issuer, string basicRoleName, int unactivatedUsersLifetime, int deleteInvalidPermissionsDelay)
        {
            Issuer = issuer;
            BasicRoleName = basicRoleName;
            UnactivatedUsersLifetime = unactivatedUsersLifetime;
            DeleteInvalidPermissionsDelay = deleteInvalidPermissionsDelay;
        }

        public string Issuer { get; }

        public string BasicRoleName { get; }

        public int UnactivatedUsersLifetime { get; }
        public int DeleteInvalidPermissionsDelay { get; }
    }
}
