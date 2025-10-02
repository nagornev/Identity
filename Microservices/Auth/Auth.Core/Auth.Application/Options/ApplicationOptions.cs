namespace Auth.Application.Options
{
    public class ApplicationOptions
    {
        public ApplicationOptions(string issuer, Access roles, int unactivatedUsersLifetime)
        {
            Issuer = issuer;
            Roles = roles;
            UnactivatedUsersLifetime = unactivatedUsersLifetime;
        }

        public string Issuer { get; }

        public Access Roles { get; }

        public int UnactivatedUsersLifetime { get; }

        public class Access
        {
            public Access(Role owner, Role basic)
            {
                Owner = owner;
                Basic = basic;
            }

            public Role Owner { get; }
            public Role Basic { get; }
        }

        public class Role
        {
            public Role(string name, List<Scope> scopes)
            {
                Name = name;
                Scopes = scopes;
            }

            public string Name { get; }

            public IReadOnlyCollection<Scope> Scopes { get; }
        }

        public class Scope
        {
            public Scope(string action, string resource, string description)
            {
                Action = action;
                Resource = resource;
                Description = description;
            }

            public string Name => $"{Action}:{Resource}";

            public string Action { get; }

            public string Resource { get; }

            public string Description { get; }
        }
    }
}
